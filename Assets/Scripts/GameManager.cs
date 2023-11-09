using System.Collections;
using System.Linq;
using Characters.Enemy;
using Characters.Player;
using Environment;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the game manager.
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Property to access the ground system.
    /// </summary>
    public GroundSystem GroundSystem
    {
        get
        {
            // If no ground system is assigned then find one from the scene.
            if (!_groundSystem)
                _groundSystem = FindObjectOfType<GroundSystem>();

            return _groundSystem;
        }
    }

    /// <summary>
    /// Property for the enemy search radius.
    /// </summary>
    public float EnemySearchRadius => enemySearchRadius;

    /// <summary>
    /// Property to access the player.
    /// </summary>
    public Player Player { get; private set; }

    /// <summary>
    /// Property to access the target.
    /// </summary>
    public Transform Target { get; private set; }

    /// <summary>
    /// Property to access the enemy list.
    /// </summary>
    public Enemy[] Enemies { get; private set; }

    /// <summary>
    /// Field for enemy search radius.
    /// </summary>
    [SerializeField] private float enemySearchRadius = 5f;

    /// <summary>
    /// Field for number of enemies to be spawned.
    /// </summary>
    [SerializeField] private int numberOfEnemies = 3;

    /// <summary>
    /// Field for player prefab.
    /// </summary>
    [SerializeField] private Player playerPrefab;

    /// <summary>
    /// Field for target prefab.
    /// </summary>
    [SerializeField] private Transform targetPrefab;

    /// <summary>
    /// Field for enemy prefab.
    /// </summary>
    [SerializeField] private Enemy enemyPrefab;

    /// <summary>
    /// Field for result text on the result display panel.
    /// </summary>
    [Space(50)] [SerializeField] private TMP_Text resultText;

    /// <summary>
    /// Field for the result display panel.
    /// </summary>
    [SerializeField] private GameObject resultDisplayPanel;

    /// <summary>
    /// Variable for the ground system.
    /// </summary>
    private GroundSystem _groundSystem;

    /// <summary>
    /// Flag for resetting the game.
    /// </summary>
    private bool _resettingGame;

    /// <summary>
    /// Constant string for player winning.
    /// </summary>
    private const string PlayerWon = "Player Won !!";

    /// <summary>
    /// Constant string for player losing.
    /// </summary>
    private const string PlayerLost = "Player Lost :-(";

    private void Awake()
    {
        // Set this object as singleton if there is none.
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Add it to DDOL.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hide the result panel.
        resultDisplayPanel.SetActive(false);
        // Call the initialization function.
        Init();
    }

    /// <summary>
    /// Initialization function for the game.
    /// </summary>
    private void Init()
    {
        // Initialize the ground system.
        GroundSystem.Init();
        // Spawn enemies.
        SpawnEnemies();
        // Spawn player.
        SpawnPlayer();
        // Spawn Target
        SpawnTarget();
    }

    /// <summary>
    /// Function to spawn enemies.
    /// </summary>
    private void SpawnEnemies()
    {
        // Initialize an array for enemies. 
        Enemies = new Enemy[numberOfEnemies];
        for (var i = 0; i < numberOfEnemies; i++)
        {
            // Instantiate enemy
            Enemies[i] = Instantiate(enemyPrefab, GroundSystem.GetRandomPoint(), Quaternion.identity);
            // Subscribe to on enemy death event.
            Enemies[i].OnAgentDestroyed += OnEnemyDeath;
            // Initialize the enemy instance.
            Enemies[i].Init();
        }
    }

    /// <summary>
    /// Function to spawn player.
    /// </summary>
    private void SpawnPlayer()
    {
        // Instantiate player
        Player = Instantiate(playerPrefab, GroundSystem.GetRandomPoint(), Quaternion.identity);
        //Subscribe to on target achieved and on player destroyed events.
        Player.OnTargetAchieved += OnTargetAchieved;
        Player.OnAgentDestroyed += OnPlayerDeath;
        // Initialize the player instance.
        Player.Init();
    }

    /// <summary>
    /// Function to spawn the target.
    /// </summary>
    private void SpawnTarget()
    {
        // Instantiate the target.
        Target = Instantiate(targetPrefab, GroundSystem.GetRandomPoint(), Quaternion.identity);
    }

    /// <summary>
    /// Function called when player achieves the target.
    /// </summary>
    private void OnTargetAchieved()
    {
        // Set the result text to player won text.
        resultText.SetText(PlayerWon);
        // Unsubscribe to all the player events.
        Player.OnTargetAchieved -= OnTargetAchieved;
        Player.OnAgentDestroyed -= OnPlayerDeath;
        // Start the coroutine to reset the game.
        StartCoroutine(ResetGame());
    }

    /// <summary>
    /// Function called when player dies.
    /// </summary>
    private void OnPlayerDeath()
    {
        // Set the result text to player lost text.
        resultText.SetText(PlayerLost);
        // Unsubscribe to all the enemy events.
        foreach (var enemy in Enemies)
            enemy.OnAgentDestroyed += OnEnemyDeath;
        // Start the coroutine to reset the game.
        StartCoroutine(ResetGame());
    }

    /// <summary>
    /// Function called when enemy dies.
    /// </summary>
    private void OnEnemyDeath()
    {
        // Remove all the null enemies from the enemies list.
        var newEnemiesList = Enemies.Where(enemy => enemy);
        Enemies = newEnemiesList.ToArray();
    }

    /// <summary>
    /// Coroutine to reset the game.
    /// </summary>
    private IEnumerator ResetGame()
    {
        // If already resetting then break out of the coroutine.
        if (_resettingGame) yield break;

        // Set resetting game flag to true.
        _resettingGame = true;

        // Show the result panel.
        resultDisplayPanel.SetActive(true);
        var sceneLoading = SceneManager.LoadSceneAsync("Main Scene");
        // Wait for 3 seconds.
        yield return new WaitForSeconds(3f);
        // Wait for new scene to load.
        yield return sceneLoading;
        // Hide the result panel.
        resultDisplayPanel.SetActive(false);

        // Initialize the game again.
        Init();
        // Set the resetting game flag to false.
        _resettingGame = false;
    }
}