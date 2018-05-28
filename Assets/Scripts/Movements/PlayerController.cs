using Assets.Scripts.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {


    public float speed = 5;
    public float mouseSensitivity = 3;
    public float jumpHeight = 2.0f;

    private PlayerMovement scriptMove;

	// Use this for initialization
	public override void OnStartAuthority()
    {
        if (hasAuthority == false)
        {
            return;
        }
        scriptMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false)
        {
            return;
        }

        jumpHeight = 0;

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
