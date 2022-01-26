using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("References for instantiation")]
    public GameObject prefab;
    public GameObject coinPrefab;
    public GameObject sunPrefab;
    public GameObject[] spawnPoints;
    public int planetCount=25;
    Vector2 spawnPosition = Vector2.zero;
    private float radius = 1f;
    private bool isCoinSpawned = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Spawn()
    {
        int i = 0;
        isCoinSpawned = false;
        while ( i < planetCount)
        {
            float x = Random.Range(0.05f, 0.95f);
            float y = Random.Range(0.05f, 0.95f);
            Vector3 pos = new Vector3(x, y, 10.0f);
            spawnPosition = Camera.main.ViewportToWorldPoint(pos);
            //Debug.Log(spawnPosition);
            if (DetectCollisions(spawnPosition)>0) {
                continue;
            }
            else
            {
                //Deactivated coin spawning
                //if (GameObject.FindObjectOfType<GameManager>().currentWave > 0 && isCoinSpawned == false)
                //{
                //    GameObject inst = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                //    isCoinSpawned = true;
                //}
                //else
                //{
                    GameObject inst = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    inst.GetComponent<Renderer>().material.color = RandomColor();
                    i++;
                
            }
            //isCoinSpawned = false;
            
            
        }
    }
    public void SpawnSun()
    {
        
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        spawnPosition = Camera.main.ViewportToWorldPoint(pos);
        
        //if (DetectCollisions(spawnPosition) > 0)
        //{
        //    Debug.Log("Sun will collide in something...");
        //}
        //else
        //{
            
                GameObject inst = Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
                
            
        //}
    }

    private Color RandomColor()
    {
        Color color = new Color(
       Random.Range(0f, 1f),
       Random.Range(0f, 1f),
       Random.Range(0f, 1f));
       return color;
   
    }

    private int DetectCollisions(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, radius);
        return hitColliders.Length;
    }



}
