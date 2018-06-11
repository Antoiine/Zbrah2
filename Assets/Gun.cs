using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    //Probablement à changer inGame
    public float range = 100f;
    public float fireRate = 2f;
    public int damagePoint = 20;
    public int fireArm = 30;

    //Effet de lumière ou autre
    public ParticleSystem gunFireLight;
    public ParticleSystem smokeEffect;
    public ParticleSystem bulletEffect;

    public Transform spawnBullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
