using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Enemy;
using Characters.Player;
using Environment;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GroundSystem GroundSystem
    {
        get
        {
            if (!_groundSystem)
                _groundSystem = FindObjectOfType<GroundSystem>();

            return _groundSystem;
        }
    }

    public float EnemySearchRadius => enemySearchRadius;
    public Player Player { get; private set; }
    public Transform Target { get; private set; }
    public Enemy[] Enemies { get; private set; }

    [SerializeField] private float enemySearchRadius = 5f;
    [SerializeField] private int numberOfEnemies = 3;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform targetPrefab;
    [SerializeField] private Enemy enemyPrefab;

    [Space(50)] [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject resultDisplayPanel;

    private GroundSystem _groundSystem;
    private bool _resettingGame;

    private const string PlayerWon = "Player Won !!";
    private const string PlayerLost = "Player Lost :-(";

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        resultDisplayPanel.SetActive(false);
        Init();
    }

    private void Init()
    {
        GroundSystem.Init();
        SpawnEnemies();
        SpawnPlayer();
        SpawnTarget();
    }

    private void SpawnEnemies()
    {
        Enemies = new Enemy[numberOfEnemies];
        for (var i = 0; i < numberOfEnemies; i++)
        {
            Enemies[i] = Instantiate(enemyPrefab, GroundSystem.GetRandomPoint(), Quaternion.identity);
            Enemies[i].OnAgentDestroyed += OnEnemyDeath;
            Enemies[i].Init();
        }
    }

    private void SpawnPlayer()
    {
        Player = Instantiate(playerPrefab, GroundSystem.GetRandomPoint(), Quaternion.identity);
        Player.OnTargetAchieved += OnTargetAchieved;
        Player.OnAgentDestroyed += OnPlayerDeath;
        Player.Init();
    }

    private void SpawnTarget()
    {
        Target = Instantiate(targetPrefab, GroundSystem.GetRandomPoint(), Quaternion.identity);
    }

    private void OnTargetAchieved()
    {
        resultText.SetText(PlayerWon);
        StartCoroutine(ResetGame());
    }

    private void OnPlayerDeath()
    {
        resultText.SetText(PlayerLost);
        StartCoroutine(ResetGame());
    }

    private void OnEnemyDeath()
    {
        var newEnemiesList = Enemies.Where(enemy => enemy);
        Enemies = newEnemiesList.ToArray();
    }

    private IEnumerator ResetGame()
    {
        if (_resettingGame) yield break;

        _resettingGame = true;
        
        resultDisplayPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        resultDisplayPanel.SetActive(false);
        
        yield return SceneManager.LoadSceneAsync("Main Scene");
        
        Init();
        _resettingGame = false;
    }
}