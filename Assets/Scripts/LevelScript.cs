using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Pickup[] pickups = GetComponentsInChildren<Pickup>();
        uint bursts = 0;
        foreach(Pickup pickup in pickups)
        {
            if (pickup.ItemType == Pickup.PICKUP_ITEM.SPEED_BURST)
            {
                bursts++;
            }
        }
        GameStats.Instance.BurstQuantity = bursts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}