//Last edited : 30/05

using UnityEngine;

/// <summary>
/// Attach this to the Torch gameObject the player holds when equipped
/// </summary>
public class Torch : MonoBehaviour
{

    [SerializeField]
    private GD2Lib.Event m_onUseTorch;

    private int m_nbFramesElapsed = 0;

    [SerializeField] [Tooltip("Attach Player animator component component")] Animator m_playerAnimator;

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
        // raise event to know if the torch is being used or not
        m_onUseTorch.Raise(p_useTorch);

        //anim flamme de la torche en loop si equipée
        if (p_useTorch)
        {

            //anim tend la torche
            if (m_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils")) m_playerAnimator.SetTrigger("ToolsSwing");

        }
        else
        {

            //anim idleoutils
            if (m_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tools")) m_playerAnimator.SetTrigger("IdleOutils");

        }

    }

}
