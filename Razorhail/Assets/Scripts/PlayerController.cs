using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Borders
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {
        
    private Rigidbody rb;
    private float moveX, moveY;

    public Sprite white;
    public Sprite black;

    public float fireRate;
    private float nextFire;

    public float tagC;
    private float tagT;

    public float speed;
    public Borders borders;

    SpriteRenderer sprite;

    //HP kokeilut
    public float MaxHealth = 100f;
    public float CurHealth = 0f;
    public GameObject HealthBar;


    public GameObject hazard;
    public GameObject shot;
    public GameObject shot1;
    public GameObject spawnPoint;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Vector3 spawnValues;

    //Alku toiminta
    void Start ()
    {
        CurHealth = MaxHealth;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpawnWaves());
    }

    //Fysiikka koodi lasketaan joka freimi
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            ChangeShot();
            nextFire = Time.time + fireRate;
        }
        if (Input.GetButton("Jump") && Time.time > tagT)
        {
            tagT = Time.time + tagC;
            ChangeTag();
        }
        Debug.Log(gameObject.tag);

    }
    void ChangeTag()
    {
        if (gameObject.CompareTag("White"))
        {
            gameObject.tag = "Black";
            sprite.sprite = black;
        }
        else if (gameObject.CompareTag("Black"))
        {
            gameObject.tag = "White";
            sprite.sprite = white;
        }
    }
    void ChangeShot()
    {
        if (gameObject.CompareTag("Black"))
        {
            Instantiate(shot, spawnPoint.transform.position, Quaternion.Euler(90, 0, 0));
        }
        else if (gameObject.CompareTag("White"))
        {
            Instantiate(shot1, spawnPoint.transform.position, Quaternion.Euler(90, 0, 0));
        }
    }

    //Toiminta koodi fysiikan jälkeen laskettava
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, borders.xMin, borders.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, borders.yMin, borders.yMax)
        );
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PlayerController myPlayer = new PlayerController();
            speed += 5;
            Destroy(other.gameObject);
        }
    }

    public void DecreaseHealth() {
        CurHealth -= 10f;
        float CalcHealth = CurHealth / MaxHealth;
        SetHealthBar(CalcHealth);
    }

    public void SetHealthBar(float myHealth) {
        if (myHealth > 0)
        {
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, myHealth, HealthBar.transform.localScale.z);
        } else {
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, 0, HealthBar.transform.localScale.z);
            Destroy(gameObject);
        }
        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = transform.rotation;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            
        }
    }
}
