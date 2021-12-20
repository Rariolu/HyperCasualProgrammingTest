using UnityEngine;
using System.Collections;

/// <summary>
/// A script attached to an individual node within a graph
/// for the use of pathfinding.
/// </summary>
public class MapNode : MonoBehaviour
{
    /// <summary>
    /// The node's position as a Vector2.
    /// </summary>
    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
    }

    [SerializeField]
    MapNode[] neighbours;
    
    /// <summary>
    /// The MapNodes that are considered "adjacent" to this one
    /// and can be travelled to from this node.
    /// </summary>
    public MapNode[] Neighbours
    {
        get
        {
            return neighbours;
        }
    }

    float[] neighbourCosts;
    /// <summary>
    /// The costs of travelling to each respective node.
    /// </summary>
    public float[] NeighbourCosts
    {
        get
        {
            return neighbourCosts;
        }
    }

    public MapPathfindingNode pathData;

    /// <summary>
    /// Calculate the "cost" of travelling to each of the neighbouring nodes.
    /// </summary>
    void CalculateCosts()
    {
        neighbourCosts = new float[neighbours.Length];
        for (int i = 0; i < neighbours.Length; i++)
        {
            neighbourCosts[i] = Vector2.Distance(Position, neighbours[i].Position);
        }
    }

    /// <summary>
    /// Determine which node is a higher "priority" between this node and another.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int CompareTo(MapNode node)
    {
        if (pathData.f < node.pathData.f)
        {
            return -1;
        }
        else if (pathData.f > node.pathData.f)
        {
            return 1;
        }

        return 0;
    }

    // Use this for initialization
    void Start()
    {
        CalculateCosts();
        if (Map.InstanceAvailable(out Map map))
        {
            //Reset this node's pathfinding cache when requested.
            map.PathDataReset += () => { pathData = new MapPathfindingNode(); };
        }
    }
}

/// <summary>
/// A structure used within the pathfinding algorithm to store
/// extraneous data that is only relevant in that scenario.
/// </summary>
public struct MapPathfindingNode
{
    public float g;
    public float h;
    public float f;
    public bool onOpenList;
    public MapNode parent;
}