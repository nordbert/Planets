using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Start is called before the first frame update
   const float G = 1.6674f;

	public static List<Attractor> Attractors;
	public GameObject[] planets;

	public Rigidbody rb;

    private void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
	{
		planets = GameObject.FindGameObjectsWithTag("Planet");

		foreach (GameObject planet in planets)
		{
			//if (attractor != this)
			//	Attract(attractor);
			Attract(planet);
		}
	}

	

	void Attract (GameObject objToAttract)
	{
		//Rigidbody rbToAttract = objToAttract.rb;
		Rigidbody rbToAttract = objToAttract.GetComponent<Rigidbody>();

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;

		if (distance == 0f)
			return;

		float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;

		rbToAttract.AddForce(force);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
			Destroy(collision.gameObject,1f);
        }
    }
}
