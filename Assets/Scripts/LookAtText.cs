using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtText : MonoBehaviour
{

    private void Update()
    {
        // We need an alternative to this, avoid rotating on x because its weird when the player is on the lower half of the screen
        transform.LookAt(Camera.main.transform.position);
    }
}
