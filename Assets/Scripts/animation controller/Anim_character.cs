using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Anim_character : NetworkBehaviour{

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = this.transform.GetChild(1).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority == false)
        {
            return;
        }
        AnimCharacter();
	}

    [Client]
    public void AnimCharacter(){
        if (Input.GetKey("z")|| Input.GetKey(KeyCode.Q)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CmdRun();
            }
            else {
                CmdWalk();
            }
            
        }
        else
        {
            CmdIdle();
        }
    }

    [Command]
    public void CmdIdle()
    {
        anim.SetFloat("trans1", 1);
        anim.Play("Idle");
        RpcIdle();
    }
    [ClientRpc]
    void RpcIdle()
    {
        anim.SetFloat("trans1", 1);
        anim.Play("Idle");
    }

    [Command]
    public void CmdWalk(){
        anim.SetFloat("trans1", 0);
        anim.Play("Walk");
        RpcWalk();
    }
    [ClientRpc]
    void RpcWalk(){
        anim.SetFloat("trans1", 0);
        anim.Play("Walk");
    }

    [Command]
    public void CmdRun()
    {
        anim.Play("Run");
        RpcRun();
    }
    [ClientRpc]
    void RpcRun()
    {
        anim.Play("Run");
    }

}
