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

    public Sprite batonSprite;
    public Sprite tissuSprite;
    public Sprite mrcFerSprite;
    public Sprite caillouSprite;
    public Sprite gros_caillouSprite;
    public Sprite poudreSprite;

    public Sprite decraftHacheFerSprite;
    public Sprite decraftAllumetteSprite;
    public Sprite decraftTorcheSprite;
    public Sprite decraftHachePierreSprite;

    public Sprite plan_echelleSprite;

    public Sprite hacheSprite;
    public Sprite allumetteSprite;
    public Sprite hache_pierreSprite;
    public Sprite echelleSprite;
}
