using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementGun : NetworkBehaviour {

    [SyncVar]
    public Transform cam;

	void Update () {
        this.transform.rotation = Quaternion.AngleAxis(90, Vector3.right) * cam.rotation ;
	}
}
