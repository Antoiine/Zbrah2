using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (hasAuthority== false)
        {
            return;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if (hasAuthority == false)
        {
            return;
        }
        Transform playerCam = this.transform.GetChild(1);
        playerCam.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Translate(0, 1, 0);
        }
	}
}
