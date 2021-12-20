using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PathDataReset();
public class Map : MonoBehaviour
{
    #region SingletonSetup
    static Map instance;
    public static bool InstanceAvailable(out Map map)
    {
        map = instance;
        return map != null;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public PathDataReset PathDataReset;

    /// <summary>
    /// Performs an A* search to find the optimal path between the
    /// given start and end nodes.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<MapNode> GetPath(MapNode start, MapNode end)
    {
        PathDataReset();
        List<MapNode> closedList = new List<MapNode>();
        Comparison<MapNode> comparison = (n1, n2) =>
        {
            return n1.CompareTo(n2);
        };
        PriorityQueue<MapNode> openList = new PriorityQueue<MapNode>(comparison);
        openList.Enqueue(start);
        while (openList.Count > 0)
        {
            MapNode currentNode = openList.Dequeue();
            closedList.Add(currentNode);
            if (currentNode == end)
            {
                return GetFoundPath(currentNode);
            }
            MapNode[] neighbours = currentNode.Neighbours;
            foreach (MapNode neighbour in neighbours)
            {
                if (closedList.Contains(neighbour))
                {
                    continue;
                }
                float cost = Vector2.Distance(currentNode.Position, neighbour.Position);
                float g = currentNode.pathData.g + cost;
                float h = Vector2.Distance(neighbour.Position, end.Position);
                float f = g + h;
                if (f < neighbour.pathData.f || !neighbour.pathData.onOpenList)
                {
                    neighbour.pathData.f = f;
                    neighbour.pathData.g = g;
                    neighbour.pathData.h = h;
                    neighbour.pathData.parent = currentNode;
                    if (!neighbour.pathData.onOpenList)
                    {
                        neighbour.pathData.onOpenList = true;
                        openList.Add(neighbour);
                    }
                }
            }
        }
        return GetFoundPath(null);
    }

    /// <summary>
    /// Return a list of nodes that represent a path
    /// going from one node's "parent" to the next.
    /// </summary>
    /// <param name="end"></param>
    /// <returns></returns>
    List<MapNode> GetFoundPath(MapNode end)
    {
        List<MapNode> foundPath = new List<MapNode>();
        while (end != null)
        {
            foundPath.Add(end);
            end = end.pathData.parent;
        }
        foundPath.Reverse();
        return foundPath;
    }
}
