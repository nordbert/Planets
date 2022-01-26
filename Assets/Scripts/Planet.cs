using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Planet : MonoBehaviour
{




   
    public RaycastHit raycastHit;
    private int touchesLeft;
    private Vector3 actualScale;             // scale of the object at the begining of the animation
    private Vector3 targetScale;     // scale of the object at the end of the animation
    public float expMultiplier;//=5f;

    //public AudioClip explode;
    public AudioClip pop;
    
    // Start is called before the first frame update
    void Start()
    {
        expMultiplier = PlayerPrefs.GetFloat("Multiplier");
        

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
                if ((raycastHit.transform.gameObject.tag == "Planet") && (touchesLeft!=0))
                {
                    PlayerPrefs.SetInt("Touches", 0);
                    
                    //Exploding(raycastHit.transform.gameObject);
                    StartCoroutine("Explode", raycastHit.transform.gameObject);                    
                }
             
            }     

        }
           
            
       
    }

    private void DestroyPlanet(GameObject planet)
    {
        //planet.GetComponent<AudioSource>().Play();
        planet.GetComponent<AudioSource>().PlayOneShot(pop);
        Destroy(planet,5f);
        Debug.Log(planet + " destroyed.");
    }
    
  


    IEnumerator Explode(GameObject target)
    {
        target.tag = "ExplodingPlanet";
        float scaleDuration = 5;                                //animation duration in seconds
        actualScale = target.transform.localScale;
        targetScale = actualScale * expMultiplier;
        
        //for (float t = 0; t < 1; t += Time.deltaTime / scaleDuration)
        //{
            
        //    target.transform.localScale = Vector3.Lerp(actualScale, targetScale, t);
            
        //    yield return null;
        //}
        float t = 0;
        while (t<1)
        {
            //target.GetComponent<AudioSource>().PlayOneShot(explode);
            target.transform.localScale = Vector3.Lerp(actualScale, targetScale, t);
            t += Time.deltaTime / scaleDuration;
            if (actualScale == targetScale)
            {
                yield break;
            }
            yield return null;

        }
        //Debug.Log("Coroutine ended.");
        //target.GetComponent<Renderer>().material.color = Color.red;
        target.GetComponent<MeshRenderer>().enabled = false;
        target.GetComponent<SphereCollider>().enabled = false;
        target.tag = "Exploded";        
        DestroyPlanet(target);
        

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Planet")
        {
            //collision.transform.tag = "Explodable";            
            //Debug.Log("Collision detected! With: " + collision.transform.name);
            StartCoroutine("Explode", collision.gameObject);

        }
    }

    
 






}
