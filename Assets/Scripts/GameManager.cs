using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int touchesLeft=1;
    private int currentTouches;
    public int currentWave = 0;
    private int recordWave;

    private float multiplier;

    public Text touchText;
    public Text record;
    public Text remainingPlanets;
    public Text waveCounter;
    public Button restartButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        //disable screentimeout
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        PlayerPrefs.SetInt("Touches", touchesLeft);        
        recordWave = PlayerPrefs.GetInt("RecordWave");
        PlayerPrefs.SetFloat("Multiplier",4.5f);
        
        
        

    }

    // Update is called once per frame
    void Update()
    {

        

        if (SceneManager.GetActiveScene().name == "Game" )
        {
            uiInfo();
            //CheckGameState();
            //Debug.Log("Planet:"+ GameObject.FindGameObjectsWithTag("Planet").Length + "Exploded:" + GameObject.FindGameObjectsWithTag("Exploded").Length + "ExplodingPlanet" + GameObject.FindGameObjectsWithTag("ExplodingPlanet").Length);
            
            
        }

        if(SceneManager.GetActiveScene().name == "GameExplode")
        {
            uiInfo();
            CheckGameStats2();
        }
        
    }
    private void uiInfo()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        remainingPlanets.text = planets.Length.ToString();
        touchText.text = currentTouches.ToString();
        

    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameExplode");
    }

    public void CheckGameState()
    {
        CountWaves();
        currentTouches = PlayerPrefs.GetInt("Touches");   
        if ((GameObject.FindGameObjectsWithTag("ExplodingPlanet").Length != 0 && GameObject.FindGameObjectsWithTag("Planet").Length != 0) || (currentTouches != 0 && GameObject.FindGameObjectsWithTag("Planet").Length != 0))
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("ExplodingPlanet").Length);
           // Debug.Log("Game is running");
        }
        else if (GameObject.FindGameObjectsWithTag("Planet").Length > 0 && GameObject.FindGameObjectsWithTag("ExplodingPlanet").Length ==0 && currentTouches == 0 )
        {
            //Debug.Log("You lose.");
            StopAllCoroutines();
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
        else if (GameObject.FindGameObjectsWithTag("Planet").Length ==0 && GameObject.FindGameObjectsWithTag("ExplodingPlanet").Length ==0 )
        {
            
            NextWave();
            //Debug.Log("You win!");
        }
        

        
    }

    public void CheckGameStats2()
    {
        CountWaves();
        currentTouches = PlayerPrefs.GetInt("Touches");
        if ((GameObject.FindGameObjectsWithTag("Exploded").Length != 0 && GameObject.FindGameObjectsWithTag("Planet").Length != 0) || (currentTouches != 0 && GameObject.FindGameObjectsWithTag("Planet").Length != 0))
        {
            //Debug.Log(GameObject.FindGameObjectsWithTag("ExplodingPlanet").Length);
            //Debug.Log("Game is running");
        }
        else if (GameObject.FindGameObjectsWithTag("Planet").Length > 0 && GameObject.FindGameObjectsWithTag("Exploded").Length == 0 && currentTouches == 0)
        {
            //Debug.Log("You lose.");
            
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
        else if (GameObject.FindGameObjectsWithTag("Planet").Length == 0 && GameObject.FindGameObjectsWithTag("Exploded").Length == 0)
        {

            NextWave();
           // Debug.Log("You win!");
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
    private void DestroyTempObjects()
    {
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        if (sun)
        {
            Destroy(sun);
        }

    }
    
    private void NextWave()
    {

        //StopAllCoroutines();
        //Destroy temporarly objects if they exist
        DestroyTempObjects();
        


        //set touchcount to default again
        if (GameObject.FindGameObjectsWithTag("Exploded").Length ==0)
        {

            currentWave += 1;
            if (GameObject.FindObjectOfType<Spawner>().planetCount > 16)
            {
                
                GameObject.FindObjectOfType<Spawner>().planetCount -= 4;
                multiplier = PlayerPrefs.GetFloat("Multiplier");
                multiplier += 0.2f;
                PlayerPrefs.SetFloat("Multiplier",multiplier);
                //GameObject.FindObjectOfType<Spawner>().Spawn();
            }
            GameObject.FindObjectOfType<Spawner>().Spawn();

            PlayerPrefs.SetInt("Touches", touchesLeft);
        }
        
    }

    private void CountWaves()
    {
        
        
        waveCounter.text = "Wave " + currentWave ;
        //waveCounter.gameObject.SetActive(true);
        recordWave = PlayerPrefs.GetInt("RecordWave");
        if(currentWave > recordWave)
        {
            recordWave = currentWave;
            PlayerPrefs.SetInt("RecordWave", recordWave);
        }
        record.text = "Record: " + recordWave;
    }
    public void CoinAction()
    {
        float rnd = Random.Range(0, 100);
        if (rnd < 50 )
        {
            currentTouches += 1;
            PlayerPrefs.SetInt("Touches", currentTouches);
            Debug.Log("Won extra touch.");
        }
        else
        {
            GameObject.FindObjectOfType<Spawner>().SpawnSun();
            
        }
    }

    IEnumerator StartNextWave()
    {
        Debug.Log("Coroutinestarted.....................................................................................");
        yield return new WaitForSecondsRealtime(2f);
        Spawner spawner = GameObject.FindObjectOfType<Spawner>();
        spawner.Invoke("Spawn",1f);
        //GameObject.FindObjectOfType<Spawner>().Spawn();
        currentWave += 1;
        
        //set touchcount to default again
        PlayerPrefs.SetInt("Touches", touchesLeft);

    }

    
}
