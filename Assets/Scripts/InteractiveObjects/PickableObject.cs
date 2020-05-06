
using UnityEngine;
using TMPro;
using InventoryNS.Utils;
using System;
using static Item;

/// <summary>
/// parent class for all pickable or interactive objects the player can encounter
/// (PREVIOUSLY ItemWorld)
/// </summary>
public class PickableObject : MonoBehaviour
{
    protected SpriteRenderer m_spriteRenderer;

    protected int m_amount = 1;

    protected TextMeshPro m_uiSpriteText;

    // is stackable : component or tool
    protected bool m_isStackable = false;

    protected bool IsStackable
    {
        get { return m_isStackable; }
        set { m_isStackable = value; }
    }

    /// <summary>
    /// Spawns the item in the world, and sets up its associated gameobject
    /// </summary>
    /// <param name="p_pos"> its position </param>
    /// <param name="p_go"> the gameobject that needs to be set up </param>
    /// <returns> returns an object of this type ? </returns>
    public PickableObject SpawnItemWorld(Vector3 p_pos, GameObject p_go)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, p_pos, Quaternion.identity);

        PickableObject pickableObj = transform.GetComponent<PickableObject>();
        //pickableObj.SetUIItem(p_sprite);
        
        pickableObj.SetGameObject(p_go);

        return pickableObj;

    }

    /// <summary>
    /// Drop the item from the inventory to the ground 
    /// Calls in the SpawnItemWorld func to set up the gameObject
    /// </summary>
    /// <param name="p_dropPos"> the position from which you want to throw it away </param>
    /// <param name="p_go"> the prefab associated </param>
    /// <returns> returns an object of this type ? </returns>
    protected PickableObject DropItem(Vector3 p_dropPos, GameObject p_go)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        PickableObject pickableObj = SpawnItemWorld(p_dropPos + randomDir * 2f, p_go);
        pickableObj.GetComponent<Rigidbody>().AddForce(randomDir * 3f, ForceMode.Impulse);
        return pickableObj;
    }


    /// <summary>
    /// Call this to create UI form of the item from the runtime script associated
    /// </summary>
    /// <param name="p_sprite"> the sprite to render </param>
    protected void SetUIItem(Sprite p_sprite)
    {
        // estce que m_amount referencera a l'instance de la classe fille ? ou c'est une valeur commune a toutes
        m_spriteRenderer.sprite = p_sprite;
        if (m_amount > 1)
        {
            m_uiSpriteText.SetText(m_amount.ToString());
        }
        else
        {
            m_uiSpriteText.SetText("");
        }
    }

    /// <summary>
    /// Instantiate prefab in the world space at this transform's position
    /// </summary>
    /// <param name="p_go"></param>
    protected void SetGameObject(GameObject p_go)
    {
        Instantiate(p_go, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

        p_go.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// generate a class item to interact with inventory
    /// </summary>
    /// <returns></returns>
    protected Item GenerateItem()
    {
        Item item = new Item
        {

            //itemType = losange,
            amount = m_amount,
            // is stackable switch to bool

        };
        return item;
    }

    protected void DestroySelf()
    {
        Destroy(gameObject);
    }


    // void initialize object ?

    // in child class, use base. to override the inherited fonction

}
