using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Exploded" && isActive)
        {
            isActive = false; 
            GameObject.FindObjectOfType<GameManager>().CoinAction();
            Destroy(gameObject);
        }
    }
}
