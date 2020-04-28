using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public float sensitivity = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calcul de la vélocité du mouv du joueur en un vecteur 3D
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;


        //calcul rotation du joueur en un vecteur 3D
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRot, 0) * sensitivity;


        //calcul rotation de la camera en un vecteur 3D

        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * sensitivity;

    }
}
