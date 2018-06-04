using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

    public string pseudo = "NoPseudo";
    public GameObject playerUnitPrefab;
    GameObject myPlayerUnit;

    //Le playerObject est spawn à une des spawnPositions définies dans le jeu


    void Start() {
        if (isLocalPlayer == false)
        {
            return;
        }
        CmdSpawnMyUnit();
        //Debug.Log(this.GetComponent<NetworkIdentity>().netId.ToString());
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

    /*Demande au réseau de spawn une entité en donnant au client "l'autorisation"
     * Le playerUnit sera spawn au même endroit que le playerObject
    */
    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = (GameObject)Instantiate(playerUnitPrefab,transform);
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
