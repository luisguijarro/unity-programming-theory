using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject spaceShipUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI gameOverScoreValueText;
    //[SerializeField] private int playerLives; // Must be change with Game dificult.
    [SerializeField] private int metersTraveled;
    [SerializeField] private int score;

    [SerializeField] private bool onGame;
    [SerializeField] private bool onPause;

    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float minTimeToSpawn; // Must be change with Game dificult.
    [SerializeField] private AudioSource mainAudioSource;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.gameOverUI.SetActive(false);
        this.pauseUI.SetActive(false);
        maxTimeToSpawn /= MainGameManager.Instance.Dificult;
        mainAudioSource.volume = MainGameManager.Instance.MusicVolume;
        this.InitGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.SetPause(!this.onPause);
        }
    }
    
    private void InitGame()
    {
        this.score = 0;
        this.onGame = true;
        StartCoroutine(SpawnEnemies());
    }

    public void SetPause(bool mustpause)
    {
        pauseUI.SetActive(mustpause);
        this.onPause = mustpause;
    }

    public void RetryGame() // for UI
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        this.onGame = false;
        this.spaceShipUI.SetActive(false);
        this.gameOverUI.SetActive(true);
        this.gameOverScoreValueText.text = score.ToString("0000000");
        MainGameManager.Instance.GameOver(score, 0, 0);
    }

    IEnumerator SpawnEnemies()
    {
        while(this.onGame)
        {
            float randomSpawnTime = Random.Range(this.minTimeToSpawn, this.maxTimeToSpawn);
            if (!this.onPause)
            {                
                int index = Random.Range(0, this.enemyPrefabs.Length);                
                SpawnEnemy(index);                
            }
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    public void SpawnEnemy(int enemyType)
    {
        Vector3 position = new Vector3(Random.Range(-107f, 107f), 0, 940);   
        this.SpawnEnemy(enemyType, position);
    }

    public void SpawnEnemy(int enemyType, Vector3 position)
    {
        //Vector3 rotation = this.player.transform.position - position;
        this.SpawnEnemy(enemyType, position, this.enemyPrefabs[enemyType].transform.rotation);
    }

    public void SpawnEnemy(int enemyType, Vector3 position, Vector3 rotation)
    {
        this.SpawnEnemy(enemyType, position, Quaternion.Euler(rotation));
    }

    public void SpawnEnemy(int enemyType, Vector3 position, Quaternion rotation)
    {
        Instantiate(this.enemyPrefabs[enemyType], position, rotation);
    }

    public GameObject GetEnemyPrefab(int index)
    {
        if (index<this.enemyPrefabs.Length)
        {
            return this.enemyPrefabs[index];
        }
        return null;
    }

    public void IncrementScore(int score)
    {
        this.score++;
        this.scoreValueText.text = this.score.ToString("0000");
    }

    public int Score
    {
        get { return this.score; }
    }

    public GameObject Player
    {
        get { return this.player; }
    }

    public bool OnGame
    {
        get { return this.onGame; }
    }

    public bool OnPause
    {
        get { return this.onPause; }
    }
}
