using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to the Torch gameObject the player holds when equipped
/// Ignite your torch and then watch the world burn !
/// If you have a problem with anything, attach this script to you and watch this thing burn
/// </summary>
public class Torch : MonoBehaviour
{

    [SerializeField]
    private GD2Lib.Event m_onUseTorch;

    private int m_nbFramesElapsed = 0;


    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            m_nbFramesElapsed++;

            //rester appuyé = "allumer" et utiliser la torche devant soi
            if (m_nbFramesElapsed > 20)
            {
                TorchIgnited(true);
            }
        } else
        {
            m_nbFramesElapsed = 0;
            TorchIgnited(false);
        }
            

    }

    private void TorchIgnited(bool p_useTorch)
    {
        //anim flamme de la torche en loop si equipée
        if (p_useTorch)
        {
            Debug.Log("Salameche attaque flammeche !");

            m_onUseTorch.Raise(p_useTorch);

            transform.rotation = transform.parent.rotation;


        }
        else
        {
            // Default rotation
            transform.rotation = Quaternion.LookRotation(new Vector3(0, transform.parent.position.y, 0));
        }

    }

}
