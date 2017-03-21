using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    private Rigidbody rb;
    
    public float speed;
    public float aika;
    public float xMin, xMax, yMin, yMax;
    private float x;
    private float z;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
   
    void Update () {
		
	}
    void FixedUpdate()
    {
        if (Time.time > aika)
        {
            aika = Time.time + 3;
            movement = new Vector3(Random.Range(-4.75f, 4.75f), 0.0f, Random.Range(-8.5f, 8.5f));
        }
        rb.velocity = movement * speed;

        rb.position = new Vector3
       (
           Mathf.Clamp(rb.position.x, xMin, xMax),
           0.0f,
           Mathf.Clamp(rb.position.z, yMin, yMax)
       );
    }
}
