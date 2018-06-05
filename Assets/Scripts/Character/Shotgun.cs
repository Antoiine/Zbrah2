using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : NetworkBehaviour {

    //Probablement à changer inGame
    public float range = 100f;
    public float fireRate = 2f;
    public int damagePoint = 20;


    //Effet de lumière ou autre
    public ParticleSystem gunFireLight;
    public ParticleSystem smokeEffect;

    public Transform spawnBullet;

    public Camera characterCamera;

    public float nextTimeToFire = 0f;


    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (hasAuthority == false)
        {
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        //Light au moment du tir
        CmdspawnFireEffect();


        RaycastHit _hit;
        if (Physics.Raycast(characterCamera.transform.position, characterCamera.transform.forward, out _hit, range))
        {
            if (_hit.transform.tag == "Character")
            {                
                CmdGiveDamage(damagePoint, _hit.transform.gameObject);
            }
            else
            {
                RpcSpawnSmokeEffect(_hit.point,_hit.normal);
            }
        }
    }

    //Demande au serveur de traiter les dommages
    
    

    [Command]
    public void CmdGiveDamage(int _damageAmount, GameObject _target)
    {
        _target.GetComponent<Target>().TakeDamage(_damageAmount);
    }

    [Command]
    public void CmdspawnFireEffect()
    {
        Debug.Log("Fire");
        gunFireLight.Play();
        RpcSpawnFireEffect();
    }

    [ClientRpc]
    void RpcSpawnFireEffect()
    {
        gunFireLight.Play();
    }


    [Command]
    public void CmdspawnSmokeEffect(Vector3 _point, Vector3 _normal)
    {
        Debug.Log("Smoke");
        Instantiate(smokeEffect, _point, Quaternion.LookRotation(_normal));
        RpcSpawnSmokeEffect(_point,_normal);
    }

    [ClientRpc]
    void RpcSpawnSmokeEffect(Vector3 _point, Vector3 _normal)
    {
        Instantiate(smokeEffect, _point, Quaternion.LookRotation(_normal));
    }


    /*
    void SpawnBulletParticule()
    {
        Debug.Log("spawn");

        //ParticleSystem qui correspond aux tir de balle
        GameObject bulletParticule = null;

        //Récupère le point de spawn des tirs (Bout du canon)
        Transform _spawnBulletParticule = transform.GetChild(0).GetChild(0).GetChild(0);

        //Créer les particules en jeu, suivent automatiquement le point de tir
        GameObject _currentBulletParticule = Instantiate(bulletParticule, _spawnBulletParticule.position, _spawnBulletParticule.rotation);
        _currentBulletParticule.transform.parent = _spawnBulletParticule.transform;
    }*/

}
