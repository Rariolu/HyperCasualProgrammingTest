using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script attached to the player to be used for managing
/// directional control and collisions.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MapObject
{
    #region SingletonSetup
    static Player instance;
    public static bool InstanceAvailable(out Player player)
    {
        player = instance;
        return instance != null;
    }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    #endregion

    #region Components

    Animator animator;
    Animator Animator
    {
        get
        {
            if (animator == null)
            {
                return animator = GetComponent<Animator>();
            }
            return animator;
        }
    }

    Rigidbody2D body;
    Rigidbody2D RigidBody
    {
        get
        {
            if (body == null)
            {
                return body = GetComponent<Rigidbody2D>();
            }
            return body;
        }
    }

    #endregion

    #region UnityEngineFields

    [SerializeField]
    float baselineSpeed = 10f;

    [SerializeField]
    DIR initialDirection = DIR.DOWN;

    [SerializeField]
    float slowdownPercentage = 0.1f;

    [SerializeField]
    float slowdownReturnDelay = 5f;

    [SerializeField]
    float speedUpDuration = 1f;

    #endregion

    bool alreadySlowingDown = false;

    bool canSpeedUp = true;

    DIR currentDir;
    /// <summary>
    /// The current direction that the player is moving towards,
    /// setting this property affects animation and physics.
    /// </summary>
    DIR CurrentDirection
    {
        get
        {
            return currentDir;
        }
        set
        {
            currentDir = value;
            currentDirVector = directionVectors[currentDir];
            RigidBody.velocity = currentDirVector * Speed;
            Animator.SetTrigger(currentDir.ToString());
        }
    }

    Vector2 currentDirVector;

    Dictionary<DIR, Vector2> directionVectors = new Dictionary<DIR, Vector2>()
    {
        {DIR.DOWN, Vector2.down},
        {DIR.LEFT, Vector2.left},
        {DIR.RIGHT, Vector2.right},
        {DIR.UP, Vector2.up}
    };

    float slowedSpeed;
    float slowdownTimer = 0f;

    float speed;
    float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
            RigidBody.velocity = currentDirVector * Speed;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.GameObjectIs(TAG.WALL))
        {
            CurrentDirection = CurrentDirection.Negate();
            //currentDirection *= -1f;
            StartCoroutine(SlowDown());
        }
        else if (collision.gameObject.GameObjectIs(TAG.ENEMY))
        {
            GameStats.Instance.EndState = END_STATE.LOSE;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.GameObjectIs(TAG.PICKUP))
        {
            Pickup pickupScript = collision.GetComponent<Pickup>();
            if (pickupScript != null)
            {
                switch (pickupScript.ItemType)
                {
                    case Pickup.PICKUP_ITEM.COIN:
                    {
                        GameStats.Instance.Coins++;
                        break;
                    }
                    case Pickup.PICKUP_ITEM.SPEED_BURST:
                    {
                        GameStats.Instance.SpeedBursts++;
                        break;
                    }
                    case Pickup.PICKUP_ITEM.AVOIDED:
                    {
                        StartCoroutine(SlowDown(true));
                        break;
                    }
                }
                pickupScript.PickedUp();
            }
        }
    }

    /// <summary>
    /// Suddenly reduces the player's speed and then gradually increase it again until
    /// it returns to the baseline.
    /// </summary>
    /// <param name="extraSlow"></param>
    /// <returns></returns>
    IEnumerator SlowDown(bool extraSlow = false)
    {
        slowdownTimer = 0f;
        float mult = extraSlow ? 0.25f : 1f;
        Speed = baselineSpeed * slowdownPercentage * mult;
        if (!alreadySlowingDown)
        {
            alreadySlowingDown = true;
            slowedSpeed = Speed;
            while (slowdownTimer < slowdownReturnDelay && alreadySlowingDown)
            {
                slowdownTimer += Time.deltaTime;
                Speed = Mathf.Lerp(slowedSpeed, baselineSpeed, slowdownTimer / slowdownReturnDelay);
                yield return 0;
            }
            Speed = baselineSpeed;
            alreadySlowingDown = false;
        }
    }

    /// <summary>
    /// Increase the player's speed to 1.5x the baseline, ignoring any ongoing slowdown processes,
    /// before returning to the baseline after a given time has elapsed.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpeedUp()
    {
        if (canSpeedUp)
        {
            alreadySlowingDown = false;
            canSpeedUp = false;

            Speed = baselineSpeed * 1.5f;

            yield return new WaitForSeconds(speedUpDuration);

            Speed = baselineSpeed;

            canSpeedUp = true;
        }
    }

    void Start()
    {
        Speed = baselineSpeed;
        CurrentDirection = initialDirection;

        if (UIManager.InstanceAvailable(out UIManager uiManager))
        {
            //Set the current direction by the one based on the directional button.
            uiManager.DirectionButtonPressed += (dir) => { CurrentDirection = dir; };
        }
    }

    /// <summary>
    /// Utilise a speed burst if there are any available.
    /// </summary>
    public void UseBurst()
    {
        if (GameStats.Instance.SpeedBursts > 0)
        {
            GameStats.Instance.SpeedBursts--;
            StartCoroutine(SpeedUp());
        }
    }
}
