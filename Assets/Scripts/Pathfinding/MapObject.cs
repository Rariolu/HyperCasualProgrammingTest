using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that can traverse the node map physically within
/// the game world.
/// </summary>
public class MapObject : MonoBehaviour
{
    [SerializeField]
    MapNode startNode;

    MapNode currentNode;
    /// <summary>
    /// The map node that best represents the player's current position.
    /// </summary>
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

    /// <summary>
    /// Check if the collided gameobject has a MapNode
    /// component, in which case set that as the current node.
    /// </summary>
    /// <param name="obj"></param>
    void NodeHit(GameObject obj)
    {
        MapNode newNode = obj.GetComponent<MapNode>();
        if (newNode != null)
        {
            currentNode = newNode;
        }
    }
}
