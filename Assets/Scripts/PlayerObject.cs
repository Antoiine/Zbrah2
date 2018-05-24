using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

    public string pseudo = "NoPseudo";
    public GameObject playerUnitPrefab;
    GameObject myPlayerUnit;

    void Start() {
        if (isLocalPlayer == false)
        {
            return;
        }
        CmdSpawnMyUnit();
    }





    void Update() {
        if (isLocalPlayer == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            CmdSendModification("newPseudo");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CmdMoveUnitUp();
        }
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(playerUnitPrefab);
        myPlayerUnit = go;
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdMoveUnitUp()
    {
        if (myPlayerUnit == null)
        {
            return;
        }
        myPlayerUnit.transform.Translate(0, 1, 0);
        RpcMoveUnitUp(myPlayerUnit);
    }
    [ClientRpc]
    void RpcMoveUnitUp(GameObject _playerUnit)
    {
        _playerUnit.transform.Translate(0, 1, 0);
    }


    [Command]
    void CmdSendModification(string _name) {
        pseudo = _name;
        RpcSendModification(_name);
    }

    [ClientRpc]
    void RpcSendModification(string _name)
    {
        pseudo=_name;
    }
}
