using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGun : MonoBehaviour {

    public Transform cam;


	void Update () {
        this.transform.rotation = Quaternion.AngleAxis(90, Vector3.right) * cam.rotation ;
	}
}
