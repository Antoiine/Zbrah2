using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {

    //Probablement à changer inGame
    public float range = 100f;
    public float fireRate = 2f;
    public int damagePoint = 20;

    public Camera characterCamera;

    public float nextTimeToFire = 0f;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }


    void Shoot()
    {

        RaycastHit _hit;
        if (Physics.Raycast(characterCamera.transform.position, characterCamera.transform.forward, out _hit, range))
        {
            if (_hit.transform.tag == "Character")
            {
                Debug.Log("avant"+ _hit.transform.GetComponent<Target>().currentHealthPoint);
                _hit.transform.GetComponent<Target>().CmdTakeDamage(damagePoint);
                Debug.Log("après" + _hit.transform.GetComponent<Target>().currentHealthPoint);

            }
        }
    }


    void SpawnBulletParticule()
    {
        Debug.Log("spawn");

        //ParticleSystem qui correspond aux tir de balle
        GameObject bulletParticule = null;

        //Récupère le point de spawn des tirs (Bout du canon)
        Transform _spawnBulletParticule = transform.GetChild(0);

        //Créer les particules en jeu, suivent automatiquement le point de tir
        GameObject _currentBulletParticule = Instantiate(bulletParticule, _spawnBulletParticule.position, _spawnBulletParticule.rotation);
        _currentBulletParticule.transform.parent = _spawnBulletParticule.transform;
    }

}
