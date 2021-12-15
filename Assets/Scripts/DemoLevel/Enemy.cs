using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    public class Enemy : MapObject
    {
        bool chasing = true;

        [SerializeField]
        MapNode currentNode;

        [SerializeField]
        float targetRetrieveInterval = 2f;

        IEnumerator Chase()
        {
            while(chasing)
            {
                MapNode target = currentNode;
                Player player;
                if (Player.InstanceAvailable(out player))
                {
                    target = player.CurrentMapNode;
                }

                Vector3 currentPosition = transform.position;
                Vector3 targetPosition = target != null ? target.transform.position : currentPosition;

                float t = 0;


                while (Vector3.Distance(currentPosition, targetPosition) > 1f && t < targetRetrieveInterval)
                {
                    transform.position = Vector3.Lerp(currentPosition, targetPosition, t / targetRetrieveInterval);
                    yield return 0;
                }
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            StartCoroutine(Chase());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}