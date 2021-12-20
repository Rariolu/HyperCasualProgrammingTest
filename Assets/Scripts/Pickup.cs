using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PICKUP_ITEM
    {
        PRESENT,
        SPEED_BURST,
        AVOIDED
    }

    [SerializeField]
    PICKUP_ITEM type = PICKUP_ITEM.PRESENT;

    public virtual PICKUP_ITEM ItemType
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