using System.Collections;
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

    public Sprite circleSprite;
    public Sprite squareSprite;
    public Sprite triangleSprite;
    public Sprite losangeSprite;
    public Sprite squarangleSprite;
}
