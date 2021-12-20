using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PICKUP_ITEM
    {
        COIN,
        SPEED_BURST,
        AVOIDED
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