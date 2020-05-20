using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotImageDef : MonoBehaviour
{

    [SerializeField] Sprite m_backgroundSprite;

    private void Start()
    {
        SetImage();  
        Destroy(GetComponent<Animator>(), 1.1f);

    }

    private IEnumerator SetImage()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Image>().sprite = m_backgroundSprite;
    }
}
