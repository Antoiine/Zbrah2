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

    }

    //Modifier la position d'un object sur le reseau, fonction CmdMoveUnitUp à call dans la fonction Update par exemple
    /*[Command]
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
    }*/

    //Demande au réseau de spawn une entité en donnant au client "l'autorisation"
    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = (GameObject)Instantiate(playerUnitPrefab);
        myPlayerUnit = go;
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    

    //Demande au serveur de modifier le name
    [Command]
    void CmdSendModification(string _name) {
        pseudo = _name;
        RpcSendModification(_name);
    }

    //Propager le nouveau name du l'entité à tous les clients du reseau
    [ClientRpc]
    void RpcSendModification(string _name)
    {
        pseudo=_name;
    }
}
