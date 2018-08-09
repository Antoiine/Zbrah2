using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seringue : MonoBehaviour {
    public HealthPack healthpack;

    private Target target;

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Character"){
            target=col.gameObject.GetComponent<Target>();
            target.HealMe(healthpack.amount_hp);
            healthpack.RespawnSeringue();
        }

    }
}
