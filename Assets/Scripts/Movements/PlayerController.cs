using Assets.Scripts.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 5;
    public float mouseSensitivity = 3;

    private PlayerMovement scriptMove;

	// Use this for initialization
	void Start () {
        scriptMove = GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {


        //Movement "ZQSD"
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 hDirection = transform.right * _xMov;
        Vector3 zDirection = transform.forward * _zMov;

        Vector3 _velocity = (hDirection + zDirection) * speed;
        scriptMove.SetMove(_velocity);

        //Horizontal Rotation
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _hRotation = new Vector3(0, _yRot, 0) * mouseSensitivity;
        scriptMove.SetHorizontalRotation(_hRotation);

        //Camera Rotation
        float _xRot = Input.GetAxisRaw("Mouse Y");
        scriptMove.SetCameraRotation(_xRot * mouseSensitivity);


    }
}
