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

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GameObjectIs(TAG.NODE))
            {
                MapNode newNode = collision.gameObject.GetComponent<MapNode>();
                if (newNode != null)
                {
                    currentNode = newNode;
                    Debug.Log("Current node changed");
                }
            }
        }

        protected virtual void Start()
        {
            currentNode = startNode;
        }
    }
}