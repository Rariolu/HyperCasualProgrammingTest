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

        [SerializeField]
        float baselineSpeed = 10f;

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
            }
        }
    }
}