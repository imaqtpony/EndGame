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

    //bla bla bla = event rester appuyé + de 20 frames ? ou je fais la detect ici
    //if bla bla bla TorchIgnited(true)

    private void Update()
    {
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount > 0)
        {
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
        Debug.Log("Salameche attaque flammeche !");
        if (p_useTorch)
            m_onUseTorch.Raise(p_useTorch);
    }

}
