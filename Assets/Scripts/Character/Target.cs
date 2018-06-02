using UnityEngine;
using UnityEngine.Networking;
public class Target : NetworkBehaviour {

    public int startHealthPoint = 100;
    public int currentHealthPoint;

	// Use this for initialization
	void Start () {
        currentHealthPoint = startHealthPoint;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //Demande au serveur de traiter les dommages
    [Command]
    public void CmdTakeDamage(int _damageAmount)
    {
        if (_damageAmount >= currentHealthPoint)
        {
            Debug.Log("Kill");
        }
        else
        {
            currentHealthPoint -= _damageAmount;
            RpcSetHealthPoint(currentHealthPoint);
        }
        
    }

    //Propager le nouveau name du l'entité à tous les clients du reseau
    [ClientRpc]
    void RpcSetHealthPoint(int _currentHealthPoint)
    {
        currentHealthPoint = _currentHealthPoint;
    }

}
