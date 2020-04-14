using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLife : MonoBehaviour
{

    public SO ScriptableObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScriptableObj.Value++;
            //Debug.Log(ScriptableObj.VALUE);

        }
    }
}
