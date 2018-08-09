using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Target : NetworkBehaviour {

    public int startHealthPoint = 100;
    public string networkId="";
    PlayerObject myGameObject;
    public Image healthBar;


    [SyncVar]
    public int currentHealthPoint;

	// Use this for initialization
	void Start () {
        currentHealthPoint = startHealthPoint;
        networkId =  this.GetComponent<NetworkIdentity>().netId.ToString();
        myGameObject = this.transform.parent.GetComponent<PlayerObject>();
    }

    [Client]
    public void TakeDamage(int _damageAmount)
    {
        if (_damageAmount >= currentHealthPoint)
        {
            myGameObject.KillMe();
        }
        else
        {
            currentHealthPoint -= _damageAmount;
            RpcHealthBar();
        }
    }

    [ClientRpc]
    public void RpcHealthBar(){
        healthBar.fillAmount = (float)currentHealthPoint / (float)startHealthPoint;
    }

    public void HealMe(int _amout_hp){
        int new_hp = currentHealthPoint + _amout_hp;
        if (new_hp > startHealthPoint)
        {
            currentHealthPoint = startHealthPoint;
        }
        else{
            currentHealthPoint = new_hp;
        }

    }

    




}
