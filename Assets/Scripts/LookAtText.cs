using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtText : MonoBehaviour
{

    private void Update()
    {
        //transform.LookAt(Camera.main.transform.position);
        Vector3 difference = Camera.main.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));

        //Vector3 relativePos = Camera.main.transform.position - transform.position;

        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
        //Debug.LogWarning(rotation.eulerAngles.y);
    }
}
