using UnityEngine;
using System.Collections;

namespace DemoLevel
{
    public class MapObject : MonoBehaviour
    {
        [SerializeField]
        MapNode startNode;

        MapNode currentNode;
        public MapNode CurrentMapNode
        {
            get
            {
                return currentNode;
            }
        }

        protected virtual void Awake()
        {
            currentNode = startNode;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GameObjectIs(TAG.NODE))
            {
                NodeHit(collision.gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GameObjectIs(TAG.NODE))
            {
                NodeHit(collision.gameObject);
            }
        }

        void NodeHit(GameObject obj)
        {
            MapNode newNode = obj.GetComponent<MapNode>();
            if (newNode != null)
            {
                currentNode = newNode;
            }
        }

        protected virtual void Start()
        {
            
        }
    }
}