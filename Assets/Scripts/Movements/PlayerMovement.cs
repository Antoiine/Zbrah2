using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Movements
{
    public class PlayerMovement : NetworkBehaviour
    {
        //Properties
        private Vector3 nextMove = Vector3.zero;
        private Vector3 hRotation = Vector3.zero;

        private float camRotation = 0f;
        private float currentCamRotation = 0f;
        private float camRotationLimit = 85f;
        private float jumpForce = 0f;

        private Rigidbody rb;
        //private Camera cam;

        private Transform CamAndGum;
        private bool isGrounded = false;
        private float lockPos = 0f;



        //Call when the scene is loaded
        public override void OnStartAuthority()
        {
            if (hasAuthority == false)
            {
                return;
            }
            rb = this.GetComponent<Rigidbody>();

            this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            //cam = this.GetComponentInChildren<Camera>();
            CamAndGum = this.transform.GetChild(0);



        }

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.tag == "Ground")
            {
                isGrounded = true;
            }
        }

        //Set the movement for the next frame
        public void SetMove(Vector3 _movement)
        {
            nextMove = _movement;            
        }

        public void SetJump(float _jumpForce)
        {
            jumpForce = _jumpForce;
        }

        //Set the horizontal rotation for the next frame
        public void SetHorizontalRotation(Vector3 _rotation)
        {
            hRotation = _rotation;
        }

        //Set the camera rotation for the next frame
        public void SetCameraRotation(float _rotation)
        {
            camRotation = _rotation;
        }


        //Call once every frame
        public void FixedUpdate()
        {

            if (hasAuthority == false)
            {
                return;
            }

            //Movement
            if (nextMove != Vector3.zero)
            {
                rb.MovePosition(rb.position + nextMove * Time.deltaTime);
            }

            //Horizontal Rotation
            rb.MoveRotation(rb.rotation * Quaternion.Euler(hRotation));

            //Camera Rotation
            if(CamAndGum != null)
            {
                // Set our rotation and clamp it
                currentCamRotation -= camRotation;
                currentCamRotation = Mathf.Clamp(currentCamRotation, -camRotationLimit, camRotationLimit);

                //Apply our rotation to the transform of our camera
                CamAndGum.transform.localEulerAngles = new Vector3(currentCamRotation, 0f, 0f);
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isGrounded = false;
                rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                
            }           
            
        }

    }
}
