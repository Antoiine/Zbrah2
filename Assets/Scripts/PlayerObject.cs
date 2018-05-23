using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isLocalPlayer == false)
        {
            return;
        }
        //Instantiate(playerUnitPrefab);
        CmdSpawnMyUnit();
	}
    public GameObject playerUnitPrefab;
    GameObject myPlayerUnit;
	// Update is called once per frame
	void Update () {
        /*if (isLocalPlayer == false)
        {
            return;
        }*/

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdMoveUnitUp();
        }*/
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(playerUnitPrefab);
        myPlayerUnit = go;
        NetworkServer.SpawnWithClientAuthority(go,connectionToClient);
    }

    [Command]
    void CmdMoveUnitUp()
    {
        if(myPlayerUnit == null)
        {
            return;
        }
        myPlayerUnit.transform.Translate(0, 1, 0);
    }
}
