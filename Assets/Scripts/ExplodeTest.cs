using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTest : MonoBehaviour
{
    public float explosionRadius = 8f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach  (Rigidbody rigidbody in rigidbodies)
        {
            
            rigidbody.AddExplosionForce(Random.Range(500, 1000), transform.position, explosionRadius);
            //rigidbody.transform.rotation = Random.rotation;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
