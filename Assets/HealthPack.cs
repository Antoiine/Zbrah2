using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class HealthPack : NetworkBehaviour {
    public GameObject seringue;
    public int respawn_time;
    public int amount_hp;


    public void RespawnSeringue(){
        CmdDisableSeringue();
        StartCoroutine("IresetSeringue");
    }
    IEnumerator IresetSeringue()
    {
        yield return new WaitForSeconds(respawn_time);
        CmdEnableSeringue();
    }

    [Command]
    public void CmdDisableSeringue(){
        seringue.SetActive(false);
        RpcDisableSeringue();
        
    }   

    [ClientRpc]
    void RpcDisableSeringue()
    {
        seringue.SetActive(false);
    }

    [Command]
    public void CmdEnableSeringue()
    {
        seringue.SetActive(true);
        RpcEnableSeringue();

    }

    [ClientRpc]
    void RpcEnableSeringue()
    {
        seringue.SetActive(true);
    }

}
