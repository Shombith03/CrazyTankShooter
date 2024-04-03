using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;
    [SerializeField]
    private GameObject[] _obstaclePrefabs;
    [SerializeField]
    private GameObject[] _zombiePrefabs;
    [SerializeField]
    private Transform[] _lanes;
    [SerializeField]
    private float _minDelay = 10f, _maxDelay = 40f;
    private float _halfGroundSize;
    private BaseController _baseController;

    private Text _scoreText;
    private int _killCount;

    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private Text _finalScore;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _halfGroundSize = GameObject.Find("GroundBlock_Main").GetComponent<GroundBlock>()._halfLength;
        _baseController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        StartCoroutine(GenerateObstacles());
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    IEnumerator GenerateObstacles()
    {
        float timer = UnityEngine.Random.Range(_minDelay, _maxDelay) / _baseController._speed.z; // controls the timer as per the player speed
        yield return new WaitForSeconds(timer);
        CreateObstacles(_baseController.gameObject.transform.position.z + _halfGroundSize);
        StartCoroutine(GenerateObstacles());
    }

    private void CreateObstacles(float zPos)
    {
        int rand = UnityEngine.Random.Range(0, 10);
        // 60-40 chance
        if(0 <= rand && rand < 7)
        {
            int obstacleLane = UnityEngine.Random.Range(0, _lanes.Length);

            AddObstacles(new Vector3(_lanes[obstacleLane].transform.position.x, 0f, zPos), UnityEngine.Random.Range(0, _obstaclePrefabs.Length));

            int zombieLane = 0; // we need to separate the zombie lane with the obstacle lane
            if(obstacleLane == 0)
            {
                zombieLane = UnityEngine.Random.Range(0, 2) == 1 ? 1 : 2;
            }
            else if(obstacleLane == 1)
            {
                zombieLane = UnityEngine.Random.Range(0, 2) == 1 ? 0 : 2;
            }
            else if (obstacleLane == 2)
            {
                zombieLane = UnityEngine.Random.Range(0, 2) == 1 ? 1 : 0;
            }

            AddZombies(new Vector3(_lanes[zombieLane].transform.position.x, 0.15f, zPos));
        }
    }

    private void AddObstacles(Vector3 position , int type)
    {
        GameObject obstacle = Instantiate(_obstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = UnityEngine.Random.Range(0, 2) == 1; // if it is 1 then true, if it is true we will rotate

        switch (type)
        {
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : -170, 0f);
                break;
        }
    }

    private void AddZombies(Vector3 pos)
    {
        int count = UnityEngine.Random.Range(0, 3) + 1;
        for (int i = 0; i < count; i++) 
        {
            Vector3 shiftPos = new Vector3(UnityEngine.Random.Range(-.5f, .5f), 0f, UnityEngine.Random.Range(1, 10) * i); // shifts the postion, to make the zombies move 
            Instantiate(_zombiePrefabs[UnityEngine.Random.Range(0, _zombiePrefabs.Length)], pos + shiftPos * i, Quaternion.identity);
        }
    }

    public void IncreaseScore()
    {
        _killCount++;
        _scoreText.text = _killCount.ToString();
    }

    public void PauseGame()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
        _finalScore.text = "Killed: " + _killCount.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
