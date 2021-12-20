using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    public delegate void PathDataReset();
    [System.Obsolete("This class was used in the demo level and is not to be used outside of that scene.")]
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

        MapNode nodes;
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
            while(openList.Count > 0)
            {
                MapNode currentNode = openList.Dequeue();
                closedList.Add(currentNode);
                if (currentNode == end)
                {
                    return GetFoundPath(currentNode);
                }
                MapNode[] neighbours = currentNode.Neighbours;
                foreach(MapNode neighbour in neighbours)
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

        

        // Start is called before the first frame update
        void Start()
        {
            nodes = GetComponentInChildren<MapNode>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}