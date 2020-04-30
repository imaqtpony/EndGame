using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;

public class BoardBoundsScript : MonoBehaviour
{
    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;


    void OnDrawGizmos()
    {
        //// Draw a semitransparent blue cube at the transforms position
        //Gizmos.color = new Color(1, 0, 0, 0.5f);
        //Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
        //Gizmos.DrawCube(transform.localPosition, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
        //Gizmos.DrawMesh(gameObject.GetComponent<Mesh>(), transform.position);
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if(p_other.gameObject.tag == "Player")
        {
            m_boardChangeEvent.Raise(gameObject.name);
        }

    }

}
