using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    public class Enemy : MapObject
    {
        bool chasing = true;

        [SerializeField]
        float targetRetrieveInterval = 2f;

        IEnumerator Chase()
        {
            //while(chasing)
            {
                MapNode target = CurrentMapNode;
                Player player;
                if (Player.InstanceAvailable(out player))
                {
                    target = player.CurrentMapNode;
                }

                Map map;
                if (Map.InstanceAvailable(out map))
                {
                    List<MapNode> path = map.GetPath(CurrentMapNode, target);

                    foreach(MapNode node in path)
                    {
                        Vector3 currentPosition = transform.position;
                        Vector3 targetPosition = node.transform.position;

                        float t = 0;
                        float duration = 1f;

                        while (t < duration)
                        {
                            t += Time.deltaTime;
                            transform.position = Vector3.Lerp(currentPosition, targetPosition, t / duration);
                            yield return 0;
                        }
                    }
                }

                //Vector3 currentPosition = transform.position;
                //Vector3 targetPosition = target != null ? target.transform.position : currentPosition;

                //float t = 0;


                //while (Vector3.Distance(currentPosition, targetPosition) > 1f)// && t < targetRetrieveInterval)
                //{
                //    t += Time.deltaTime;
                //    transform.position = Vector3.Lerp(currentPosition, targetPosition, t / targetRetrieveInterval);
                //    yield return 0;
                //}
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            StartCoroutine(Chase());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}