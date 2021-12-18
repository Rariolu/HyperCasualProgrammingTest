using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
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
        Vector2 initialDirection = Vector2.left;

        Vector2 currentDirection;

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
                RigidBody.velocity = currentDirection * speed;
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
                UIManager uiManager;
                if (UIManager.InstanceAvailable(out uiManager))
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
                UIManager uIManager;
                if (UIManager.InstanceAvailable(out uIManager))
                {
                    uIManager.SetSpeedBursts(speedBursts);
                }
            }
        }

        void CheckDirPress(KeyCode keyCode, Vector2 dir)
        {
            if (Input.GetKey(keyCode))
            {
                RigidBody.velocity = dir * speed;
                currentDirection = dir;
            }
        }

        void DirectionButtonPressed(DIR dir)
        {
            RigidBody.velocity = directionVectors[dir] * speed;
            currentDirection = directionVectors[dir];
        }


        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            if (collision.gameObject.GameObjectIs(TAG.WALL))
            {
                currentDirection *= -1f;
                StartCoroutine(SlowDown());
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
                    }
                    pickupScript.PickedUp();
                }
            }
        }

        IEnumerator SlowDown()
        {
            slowdownTimer = 0f;
            Speed = baselineSpeed * slowdownPercentage;
            if (!alreadySlowingDown)
            {
                alreadySlowingDown = true;
                float slowedSpeed = speed;
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
            RigidBody.velocity = initialDirection * speed;
            currentDirection = initialDirection;

            UIManager uiManager;
            if (UIManager.InstanceAvailable(out uiManager))
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