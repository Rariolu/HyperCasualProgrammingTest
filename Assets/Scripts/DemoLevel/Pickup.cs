using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoLevel
{
    public class Pickup : MonoBehaviour
    {
        public enum PICKUP_ITEM
        {
            COIN
        }

        [SerializeField]
        PICKUP_ITEM type = PICKUP_ITEM.COIN;

        public PICKUP_ITEM ItemType
        {
            get
            {
                return type;
            }
        }

        public void PickedUp()
        {
            Destroy(gameObject);
        }
    }
}