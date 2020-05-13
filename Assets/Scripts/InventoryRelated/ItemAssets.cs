﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite batonSprite;
    public Sprite tissuSprite;
    public Sprite mrcFerSprite;
    public Sprite hacheSprite;
    public Sprite allumetteSprite;
}
