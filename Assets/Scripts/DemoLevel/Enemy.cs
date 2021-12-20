using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    [RequireComponent(typeof(Animator))]
    public class Enemy : MapObject
    {
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

        bool chasing = true;

        [SerializeField]
        float targetRetrieveInterval = 2f;

        enum ENEMY_ANIM_TRIGGER
        {
            DOWN,
            LEFT,
            RIGHT,
            UP,
            IDLE
        }

        IEnumerator Chase()
        {
            while(chasing)
            {
                MapNode target = CurrentMapNode;
                if (Player.InstanceAvailable(out Player player))
                {
                    target = player.CurrentMapNode;
                }

                if (Map.InstanceAvailable(out Map map))
                {
                    List<MapNode> path = map.GetPath(CurrentMapNode, target);

                    if (path.Count < 1)
                    {
                        Animator.SetTrigger(ENEMY_ANIM_TRIGGER.IDLE.ToString());
                    }

                    float chaseTime = 0f;

                    for (int i = 0; i < path.Count && chaseTime < targetRetrieveInterval; i++)//each(MapNode node in path)
                    {
                        MapNode node = path[i];
                        Vector3 currentPosition = transform.position;
                        Vector3 targetPosition = node.transform.position;

                        if (targetPosition.x < currentPosition.x)
                        {
                            Animator.SetTrigger(ENEMY_ANIM_TRIGGER.LEFT.ToString());
                        }
                        else
                        {
                            Animator.SetTrigger(ENEMY_ANIM_TRIGGER.RIGHT.ToString());
                        }

                        float t = 0;
                        float duration = 1f;

                        while (t < duration)
                        {
                            t += Time.deltaTime;
                            chaseTime += Time.deltaTime;
                            transform.position = Vector3.Lerp(currentPosition, targetPosition, t / duration);
                            yield return 0;
                        }
                    }
                }
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            StartCoroutine(Chase());
        }
    }
}