using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironementObject : MonoBehaviour
{
    //ce script pour generer les items quand l'objet du world creve, et s'autodetruire
    protected void DropMaterialOnDeath(Item p_item, float p_destroyTime)
    {

        //Item item = GenerateItem(transform.position, p_item.ItemType);
        Destroy(gameObject, p_destroyTime);
        Debug.Log("DETRUIS TOIIII");

    }



}
