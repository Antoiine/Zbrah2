using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour {

    // Use this for initialization
    public override void OnStartAuthority()
    {
        if (hasAuthority == false)
        {
            return;
        }
        Transform playerCam = this.transform.GetChild(1);
        playerCam.gameObject.SetActive(true);
    }


	
	// Update is called once per frame
	void Update () {
        if (hasAuthority == false)
        {
            return;
        }

	}
}
