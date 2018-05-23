using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Movements
{
    public class PlayerMovement : MonoBehaviour
    {
        //Properties
        private Vector3 nextMove = Vector3.zero;
        private Vector3 hRotation = Vector3.zero;

        private float camRotation = 0f;
        private float currentCamRotation = 0f;
        private float camRotationLimit = 85f;

        private Rigidbody rb;
        private Camera cam;

        

        //Call when the scene is loaded
        public void Start()
        {
            rb = this.GetComponent<Rigidbody>();
            cam = this.GetComponentInChildren<Camera>();
        }

        //Set the movement for the next frame
        public void SetMove(Vector3 _movement)
        {
            nextMove = _movement;            
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
            //Movement
            if(nextMove != Vector3.zero)
            {
                rb.MovePosition(rb.position + nextMove * Time.deltaTime);
            }

            //Horizontal Rotation
            rb.MoveRotation(rb.rotation * Quaternion.Euler(hRotation));

            //Camera Rotation
            if(cam != null)
            {
                // Set our rotation and clamp it
                currentCamRotation -= camRotation;
                currentCamRotation = Mathf.Clamp(currentCamRotation, -camRotationLimit, camRotationLimit);

                //Apply our rotation to the transform of our camera
                cam.transform.localEulerAngles = new Vector3(currentCamRotation, 0f, 0f);
            }            
            
        }

    }
}
