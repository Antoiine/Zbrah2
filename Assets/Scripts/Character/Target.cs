using UnityEngine;
using UnityEngine.Networking;
public class Target : NetworkBehaviour {

    public int startHealthPoint = 100;
    public string networkId="";

    [SyncVar]
    public int currentHealthPoint;

	// Use this for initialization
	void Start () {
        currentHealthPoint = startHealthPoint;
        networkId =  this.GetComponent<NetworkIdentity>().netId.ToString();
    }

    public void TakeDamage(int _damageAmount)
    {
        if (_damageAmount >= currentHealthPoint)
        {
            //Debug.Log("Kill" + networkId);
        }
        else
        {
            currentHealthPoint -= _damageAmount;
        }

    }




}
