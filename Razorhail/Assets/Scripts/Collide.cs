using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    public GameObject pickUp;
    public GameObject explosion;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShotEnemy") && other.gameObject.CompareTag("White"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (other.gameObject.CompareTag("Shot"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(pickUp, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
