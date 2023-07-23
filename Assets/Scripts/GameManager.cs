using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // References to player and enemy GameObjects and the ball prefab
    public GameObject player;
    public GameObject enemy;
    public GameObject ball;
    public GameObject ballPrefab;

    // Reference to the active player (player or enemy) during the round
    public GameObject activePlayer;

    // Game state variables
    public bool playerTurn = true;  // True if it's the player's turn, false if it's the enemy's turn
    public bool isPlaying = false;  // True if the game is in play mode (the ball is moving), false otherwise

    // Enemy's target angle and Y position used during the enemy's turn
    public float enemyTargetAngle;
    public float enemyTargetY;

    // Y position limits for both player and enemy
    public float minY = -3.5f;
    public float maxY = 3.5f;

    float offset = 0.4f; // Offset used to position the ball at the start of each round

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        RoundStart(); // Start the first round
    }

    // Start a new round
    public void RoundStart()
    {
        activePlayer = playerTurn ? player : enemy; // Determine the active player for this round

        // Calculate the initial position for the ball based on the active player's position and offset
        float ballX = activePlayer.transform.position.x;
        ballX += playerTurn ? offset : -offset;
        Vector2 ballPos = new(ballX, activePlayer.transform.position.y);

        // Instantiate the ball at the calculated position with no rotation
        ball = Instantiate(ballPrefab, ballPos, Quaternion.identity);
    }

    // Handle the end of a round
    public void RoundEnd(bool turn)
    {
        Destroy(ball); // Destroy the ball
        playerTurn = turn; // Switch the player turn

        // Generate random target angle and Y position for the enemy during its turn
        enemyTargetAngle = Random.Range(50f, -50f);
        enemyTargetY = Random.Range(minY, maxY);

        isPlaying = false; // Set the game state to not in play mode

        // Reset player and enemy positions to the center (Y = 0)
        player.transform.position = new Vector2(player.transform.position.x, 0);
        enemy.transform.position = new Vector2(enemy.transform.position.x, 0);

        RoundStart(); // Start the next round
    }
}
