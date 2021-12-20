using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Present : Pickup
{
    SpriteRenderer spriteRenderer;
    SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                return spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
    }

    [SerializeField]
    Sprite[] presentSprites;

    public override PICKUP_ITEM ItemType
    {
        get
        {
            return PICKUP_ITEM.PRESENT;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer.sprite = presentSprites.GetRandomElement();
    }
}