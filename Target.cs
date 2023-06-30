using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManageScript;
    public ParticleSystem explosionParticle;

    private float minForce = 12;
    private float maxForce = 15;
    private float torqueMag = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int scoreValue;
    

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManageScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(ForceVector(), ForceMode.Impulse);
        targetRb.AddTorque(TorqueRange(), TorqueRange(), TorqueRange(), ForceMode.Impulse);

        transform.position = TargetSpawnPos();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        gameManageScript.UpdateScore(scoreValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            gameManageScript.GameOver();
        }
    }

    float TorqueRange()
    {
        return Random.Range(-torqueMag, torqueMag);
    }

    Vector3 ForceVector()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    Vector3 TargetSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
