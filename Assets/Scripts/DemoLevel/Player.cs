using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
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

        [SerializeField]
        KeyCode downKey = KeyCode.S;

        [SerializeField]
        KeyCode leftKey = KeyCode.A;

        [SerializeField]
        KeyCode rightKey = KeyCode.D;

        [SerializeField]
        KeyCode upKey = KeyCode.W;

        // Start is called before the first frame update
        void Start()
        {
            speed = baselineSpeed;
            RigidBody.velocity = initialDirection * speed;
            currentDirection = initialDirection;
        }

        // Update is called once per frame
        void Update()
        {
            CheckDirPress(downKey, Vector2.down);
            CheckDirPress(leftKey, Vector2.left);
            CheckDirPress(rightKey, Vector2.right);
            CheckDirPress(upKey, Vector2.up);
        }

        void CheckDirPress(KeyCode keyCode, Vector2 dir)
        {
            if (Input.GetKey(keyCode))
            {
                RigidBody.velocity = dir * speed;
                currentDirection = dir;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GameObjectIs(TAG.WALL))
            {
                currentDirection *= -1f;
                StartCoroutine(SlowDown());
            }
        }

        IEnumerator SlowDown()
        {
            if (alreadySlowingDown)
            {
                slowdownTimer = 0f;
                speed = baselineSpeed * slowdownPercentage;
            }
            else
            {
                alreadySlowingDown = true;
                speed = baselineSpeed * slowdownPercentage;
                float slowedSpeed = speed;
                slowdownTimer = 0f;
                while (slowdownTimer < slowdownReturnDelay)
                {
                    RigidBody.velocity = currentDirection * speed;
                    slowdownTimer += Time.deltaTime;
                    speed = Mathf.Lerp(slowedSpeed, baselineSpeed, slowdownTimer / slowdownReturnDelay);
                    yield return 0;
                }
                speed = baselineSpeed;
                alreadySlowingDown = false;
            }
        }
    }
}