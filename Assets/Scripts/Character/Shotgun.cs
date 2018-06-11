using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : NetworkBehaviour {

    //Probablement à changer inGame
    private float range;
    private float fireRate;
    private int damagePoint;
    private int fireArm;
    private int bulletUsed;


    //Effet de lumière ou autre
    private ParticleSystem gunFireLight;
    private ParticleSystem smokeEffect;
    private ParticleSystem bulletEffect;

    private Transform spawnBullet;

    public Camera characterCamera;

    public float nextTimeToFire = 0f;


    private void Start()
    {

        //Récupère le gun du player
        Gun playerGun = this.transform.GetChild(0).GetChild(1).GetComponent<Gun>();

        //Get all gun stats
        fireRate = playerGun.fireRate;
        range = playerGun.range;
        damagePoint = playerGun.damagePoint;
        fireArm = playerGun.fireArm;

        //Effect gun
        gunFireLight = playerGun.gunFireLight;
        smokeEffect = playerGun.smokeEffect;         
        bulletEffect = playerGun.bulletEffect;

        spawnBullet = playerGun.spawnBullet;
       

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
            //Si il reste des balles dans le chargeur
            if (bulletUsed < fireArm){

                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                bulletUsed += 1;
            }
            else
            {
                //Animation de rechargement
                bulletUsed = 0;
                Debug.Log("reload");
            }
            
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
                CmdspawnSmokeEffect(_hit.point,_hit.normal);
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
        bulletEffect.Play();
        RpcSpawnFireEffect();
    }


    [ClientRpc]
    void RpcSpawnFireEffect()
    {
        gunFireLight.Play();
        bulletEffect.Play();
    }


    [Command]
    public void CmdspawnSmokeEffect(Vector3 _point, Vector3 _normal)
    {
        Debug.Log("Smoke");
        ParticleSystem smokePS = Instantiate(smokeEffect, _point, Quaternion.LookRotation(_normal));
        Destroy(smokePS.gameObject, 0.8f);
        RpcSpawnSmokeEffect(_point,_normal);
    }

    [ClientRpc]
    void RpcSpawnSmokeEffect(Vector3 _point, Vector3 _normal)
    {
        ParticleSystem smokePS = Instantiate(smokeEffect, _point, Quaternion.LookRotation(_normal));
        Destroy(smokePS.gameObject, 0.8f);
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
