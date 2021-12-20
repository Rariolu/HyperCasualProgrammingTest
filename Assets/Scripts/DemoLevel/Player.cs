using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
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

        bool alreadySlowingDown = false;

        Dictionary<DIR, Vector2> directionVectors = new Dictionary<DIR, Vector2>()
        {
            {DIR.DOWN, Vector2.down},
            {DIR.LEFT, Vector2.left},
            {DIR.RIGHT, Vector2.right},
            {DIR.UP, Vector2.up}
        };

        float slowdownTimer = 0f;

        [SerializeField]
        float baselineSpeed = 10f;

        [SerializeField]
        DIR initialDirection = DIR.DOWN;
        //Vector2 initialDirection = Vector2.left;

        Vector2 currentDirVector;

        DIR currentDir;
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
                RigidBody.velocity = currentDirVector * speed;
                Animator.SetTrigger(currentDir.ToString());
            }
        }

        [SerializeField]
        float slowdownReturnDelay = 1f;

        [SerializeField]
        float slowdownPercentage = 0.6f;

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
                RigidBody.velocity = currentDirVector * speed;
            }
        }

        [SerializeField]
        float speedUpDuration = 1f;

        [SerializeField]
        KeyCode downKey = KeyCode.S;

        [SerializeField]
        KeyCode leftKey = KeyCode.A;

        [SerializeField]
        KeyCode rightKey = KeyCode.D;

        [SerializeField]
        KeyCode upKey = KeyCode.W;

        uint coins;
        uint Coins
        {
            get
            {
                return coins;
            }
            set
            {
                coins = value;
                if (UIManager.InstanceAvailable(out UIManager uiManager))
                {
                    uiManager.SetCoins(coins);
                }
            }
        }

        uint speedBursts;
        uint SpeedBursts
        {
            get
            {
                return speedBursts;
            }
            set
            {
                speedBursts = value;
                if (UIManager.InstanceAvailable(out UIManager uIManager))
                {
                    uIManager.SetSpeedBursts(speedBursts);
                }
            }
        }

        void DirectionButtonPressed(DIR dir)
        {
            //RigidBody.velocity = directionVectors[dir] * speed;
            //currentDirection = directionVectors[dir];
            CurrentDirection = dir;
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
                    switch(pickupScript.ItemType)
                    {
                        case Pickup.PICKUP_ITEM.COIN:
                        {
                            Coins++;
                            break;
                        }
                        case Pickup.PICKUP_ITEM.SPEED_BURST:
                        {
                            SpeedBursts++;
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

        float slowedSpeed;

        IEnumerator SlowDown(bool extra = false)
        {
            slowdownTimer = 0f;
            float mult = extra ? 0.25f : 1f;
            Speed = baselineSpeed * slowdownPercentage * mult;
            if (!alreadySlowingDown)
            {
                alreadySlowingDown = true;
                slowedSpeed = speed;
                while (slowdownTimer < slowdownReturnDelay && alreadySlowingDown)
                {
                    //RigidBody.velocity = currentDirection * speed;
                    slowdownTimer += Time.deltaTime;
                    Speed = Mathf.Lerp(slowedSpeed, baselineSpeed, slowdownTimer / slowdownReturnDelay);
                    yield return 0;
                }
                Speed = baselineSpeed;
                //RigidBody.velocity = currentDirection * speed;
                alreadySlowingDown = false;
            }
        }

        bool canSpeedUp = true;

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

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            speed = baselineSpeed;
            CurrentDirection = initialDirection;
            //RigidBody.velocity = initialDirection * speed;
            //currentDirection = initialDirection;

            if (UIManager.InstanceAvailable(out UIManager uiManager))
            {
                uiManager.DirectionButtonPressed += DirectionButtonPressed;
            }
        }

        public void UseBurst()
        {
            SpeedBursts--;
            StartCoroutine(SpeedUp());
        }

        // Update is called once per frame
        void Update()
        {
            //CheckDirPress(downKey, Vector2.down);
            //CheckDirPress(leftKey, Vector2.left);
            //CheckDirPress(rightKey, Vector2.right);
            //CheckDirPress(upKey, Vector2.up);
        }
    }
}