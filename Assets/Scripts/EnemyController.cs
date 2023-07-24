using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;  // The speed at which the enemy can move up and down
    public float minY = -3.8f;  // The minimum Y position the enemy can move to
    public float maxY = 3.8f;   // The maximum Y position the enemy can move to

    private GameManager gameManager;  // Reference to the GameManager script
    private BallController ballController;  // Reference to the BallController script

    private void Update()
    {
        // Check if the gameManager reference is null
        if (gameManager == null)
        {
            // If null, obtain a reference to the GameManager script using the Singleton pattern
            gameManager = GameManager.instance;
        }

        // Check if the ballController reference is null
        if (ballController == null)
        {
            // If null, obtain a reference to the BallController script attached to the ball GameObject
            ballController = gameManager.ball.GetComponent<BallController>();
        }

        // Check if the game is in play mode
        if (gameManager.isPlaying)
        {
            // Check if the ball is on the enemy's side (ball's X position > 1)
            if (ballController.transform.position.x > 1f)
            {
                // Move the enemy towards the ball's Y position
                // This ensures the enemy follows the ball's vertical movement while staying on the same X position
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ballController.transform.position.y), speed * Time.deltaTime);
            }
        }
        else
        {
            // If the game is not in play mode (preparation phase or round end), and it's not the player's turn
            if (!gameManager.playerTurn && transform.position.y != gameManager.enemyTargetY)
            {
                // Move the enemy towards the target Y position (enemyTargetY) set by the GameManager
                // This ensures the enemy returns to its starting position after a round ends or during the preparation phase
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, gameManager.enemyTargetY), speed * Time.deltaTime);
            }
        }
    }
}
