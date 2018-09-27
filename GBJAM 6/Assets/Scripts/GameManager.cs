using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    
    public SceneChanger sceneChanger;
    public static GameManager instance = null;
    public GameObject enemyPrefab;
    public GameObject[] enemySpawn;
    public GameObject[] DragonSpawn;
  

    public PlayerController lifePlayer;
    public Text scorePlayerText;
    public Text highScore;
    public Text currentWave;
    public Text PlayerLifeText;
    public GameObject Paused;
    public GameObject DragonPrefab;



    public int enemyCount = 0;
    public int enemyInScene = 0;
    public int wave;
    public int enemyPerWave = 4;
    //public float delayParaActivar;
    public float timer;
    //Puntuacion

    float playerScore;
    static float playerHightScore;

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.HasKey("playerHightScore"))
        {
            playerHightScore = PlayerPrefs.GetFloat("playerHightScore");
        }
        PlayerPrefs.SetFloat("playerHightScore", playerHightScore);

    }
    // Use this for initialization
    void Start()
    {
        playerScore = 0f;
        wave = 1;
        UpdateScores();
        timer = 05f;
        Time.timeScale = 0;
    
    }

    // Update is called once per frame
    void Update() {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                Paused.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Paused.SetActive(false);
            }
        }

        if(lifePlayer.playerLife == 0)
        {
            Invoke("NextScene", 2f);
            GameOver();
        }
        WaveManager();
        if (enemyCount == enemyPerWave)
        {
            ResetTimer(5f); 
            NextWave();
            enemyCount = 0;

        }

        else
        {
            timer -= Time.deltaTime;
        }
       
       

    }

    public void Score (float score)
    {
        playerScore += score;
        UpdateScores();
    }
    public void NextWave()
    {
        FindObjectOfType<AudioManager>().Play("NextWave");
        Score(1000);
        lifePlayer.playerLife = 3;
        wave++;
        UpdateScores();
        Dificulty(wave);
        
    }


    public void ResetTimer(float time)
    {
        timer = time;
    }
     
    public void SpawnEnemy()
    {
        if(Random.value * 100  >= 95 )
        { 
            enemyInScene++;
            
            Instantiate(DragonPrefab);
            DragonPrefab.transform.position = DragonSpawn[Mathf.Clamp(Mathf.RoundToInt(Random.value * 2), 0, 2)].transform.position;
        }
        else
        {
            enemyInScene++;

            Instantiate(enemyPrefab);
            enemyPrefab.transform.position = enemySpawn[Mathf.Clamp(Mathf.RoundToInt(Random.value * 4), 0, 3)].transform.position;

        }

      
    } 
    public void UpdateScores()
    {
        scorePlayerText.text = playerScore.ToString("0");
        currentWave.text = wave.ToString("");
        highScore.text = playerHightScore.ToString("");
        
        PlayerLifeText.text = lifePlayer.playerLife.ToString(""); 
    }
    public void GameOver()
    {
        lifePlayer.speed = 0;
        if(playerScore > playerHightScore)
        {
            playerHightScore = playerScore  ;
            PlayerPrefs.SetFloat("playerHightScore", playerHightScore);
            
            UpdateScores();
            
        }
        
        
    }
    public void Dificulty(int wave)
    {
        if (wave >= 4 && wave <= 6)
        {
            enemyInScene = 0;
            enemyPerWave += 4;
            enemyPrefab.GetComponent<EnemyBehavior>().enemyLife=4;
            //delayParaActivar = 0f;
        }

        else if (wave >= 8 && wave <= 10)
        {
            enemyInScene = 0;
            enemyPerWave += 6;
            enemyPrefab.GetComponent<EnemyBehavior>().enemyLife = 5;

        }
        else if (wave >= 15 && wave <= 20)
        {
            enemyInScene = 0;
            enemyPerWave += 10;
            enemyPrefab.GetComponent<EnemyBehavior>().enemyLife = 5;

        }
        if (wave >= 20)
        {
            enemyInScene = 0;
            enemyPrefab.GetComponent<EnemyBehavior>().enemyLife = 6;
            enemyPerWave += 15;

        }
        enemyInScene = 0;
        enemyPerWave += 2;
    }
    void WaveManager()
    {
        if (timer <= 0f)
        {
            Debug.Log("Se acabo el tiempo");
            if (enemyInScene < enemyPerWave)
            {
                SpawnEnemy();
                SpawnEnemy();
            }
            if (wave >= 4 && wave <= 6)
            {
                print("timer 1");
                ResetTimer(.5f);

            }
            if (wave >= 8 && wave <= 10)
            {
                print("timer 1");
                ResetTimer(.4f);
                Debug.Log(timer);

            }
            if (wave >= 15 && wave <= 20)
            {
                print("timer 2");
                ResetTimer(.2f);
                Debug.Log(timer);

            }
            if (wave >= 20)
            {
                print("timer 3");
                ResetTimer(.1f);
                Debug.Log(timer);

            }
            else
            {
                ResetTimer(2f);
            }

        }
    }
    void NextScene()
    {
        
        sceneChanger.NextScene();
    }
    

}
