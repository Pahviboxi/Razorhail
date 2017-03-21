using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public float fireRate;
    private float nextFire;

    public GameObject shot;
    public GameObject spawnPoint;

    void Update()
    {
        if (Time.time > nextFire)
        {
            Instantiate(shot, spawnPoint.transform.position, Quaternion.Euler(90, 0, 0));
            nextFire = Time.time + fireRate;
        }

    }
}
