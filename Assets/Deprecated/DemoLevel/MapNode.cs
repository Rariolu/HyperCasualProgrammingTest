using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    [System.Obsolete("This class was used in the demo level and is not to be used outside of that scene.")]
    public class MapNode : MonoBehaviour
    {
        public Vector2 Position
        {
            get
            {
                return new Vector2(transform.position.x, transform.position.y);
            }
        }

        [SerializeField]
        MapNode[] neighbours;

        public MapNode[] Neighbours
        {
            get
            {
                return neighbours;
            }
        }

        public MapPathfindingNode pathData;

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

        void ResetPathData()
        {
            pathData = new MapPathfindingNode();
        }

        // Start is called before the first frame update
        void Start()
        {
            Map map;
            if (Map.InstanceAvailable(out map))
            {
                map.PathDataReset += ResetPathData;
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
}