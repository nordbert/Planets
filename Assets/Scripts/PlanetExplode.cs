using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetExplode : MonoBehaviour
{

    public RaycastHit raycastHit;
    //átírtam publicra...
    public int touchesLeft;
    [SerializeField]private float explosionRadius = 4f;
    public GameObject explodedPlanet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        touchesLeft = PlayerPrefs.GetInt("Touches");

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                if ((raycastHit.transform.gameObject.tag == "Planet") && (touchesLeft != 0))
                {
                    CreateExplodedPlanet(raycastHit.transform.gameObject);
                    //raycastHit.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;

                    //ExplodePlanet(raycastHit.transform.gameObject);
                    PlayerPrefs.SetInt("Touches", 0);



                }

            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Planet" || collision.gameObject.tag == "Exploded" )
        {
            Debug.Log("Collision!");
            
            CreateExplodedPlanet(gameObject);
            //ExplodePlanet(collision.gameObject);

        }
    }


    public void CreateExplodedPlanet(GameObject target)
    {
        GameObject.FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        GameObject.FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
        
        Vector3 pos = target.transform.position;
        Color targetColor = target.GetComponent<Renderer>().material.color;
        
        
        
        Debug.Log(targetColor);
        Destroy(target);
        GameObject inst = Instantiate(explodedPlanet,pos,transform.rotation);
        foreach (Renderer child in inst.GetComponentsInChildren<Renderer>())
        {
            child.material.color = targetColor;
        }
        
        
        Rigidbody[] rigidbodies = inst.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            
            rigidbody.AddExplosionForce(Random.Range(200, 400), pos, explosionRadius);
            rigidbody.transform.rotation = Random.rotation;
        }
        Destroy(inst, 5f);
    }

    

}
