using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable_Object : MonoBehaviour
{

    private bool m_pushThisOne = false;
    private bool m_playerWithinRange = false;
    private Rigidbody m_rb;

    //private Transform m_playerTransform;

    // create a GO to simulate better the directional vector for the force to apply rather than the players forward (cause he takes time to rotate)
    private GameObject m_forceDirection;


    private void Awake()
    {
        m_forceDirection = new GameObject();
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;


        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            // hit on the object
            m_pushThisOne = true;

        }

        //if hit est pas nul
        // normale de player a hit
        if (m_pushThisOne && m_playerWithinRange)
        {
            //maybe instead get a forward from the player pos to the mouse pos
            m_rb.AddForce(new Vector3(m_forceDirection.transform.forward.x,0, m_forceDirection.transform.forward.z) * 5f, ForceMode.Impulse);
            //Debug.Log("Successfully pushed obj");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            m_playerWithinRange = true;


        m_forceDirection.transform.position = other.gameObject.transform.position;
        m_forceDirection.transform.rotation = other.gameObject.transform.rotation;
        // push to center of the object
        m_forceDirection.transform.LookAt(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            m_playerWithinRange = false;
    }


}
