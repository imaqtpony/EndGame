using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// we refer all the sprite of the items in this script
/// and make an instance of them
/// </summary>
public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //game object with the item type in the world
    public Transform pfItemWorld;

    //ressources
    public Sprite m_batonSprite;
    public Sprite m_tissuSprite;
    public Sprite m_mrcFerSprite;
    public Sprite m_caillouSprite;
    public Sprite m_poudreSprite;

    //uncraft sprite
    public Sprite m_decraftHacheFerSprite;
    public Sprite m_decraftAllumetteSprite;
    public Sprite m_decraftTorcheSprite;
    public Sprite m_decraftHachePierreSprite;

    public Sprite m_plan_echelleSprite;

    public Sprite m_hacheSprite;
    public Sprite m_allumetteSprite;
    public Sprite m_hache_pierreSprite;
    public Sprite m_echelleSprite;
}
