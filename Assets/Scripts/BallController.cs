using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    float moveForce = 40f;
    float rotateSpeed = 20f;
    float minAngle = -50f;
    float maxAngle = 50f;

    private GameManager gameManager;  // Reference to the GameManager script
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component of the ball

    // Shortcuts for frequently accessed properties from the GameManager
    private bool IsPlayerTurn => gameManager.playerTurn;
    private Transform ActivePlayerTransform => gameManager.activePlayer.transform;
    private float EnemyTargetAngle => gameManager.enemyTargetAngle;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the GameManager and the Rigidbody2D component
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();

        // Activate the ball's child (visual) object
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        // If it's not the player's turn, rotate the ball 180 degrees to face the enemy's side
        if (!IsPlayerTurn)
        {
            transform.Rotate(0, 0, 180f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not in play mode (preparation phase or round end)
        if (!gameManager.isPlaying)
        {
            // Move the ball along with the player board on the Y-axis
            transform.position = new Vector2(transform.position.x, ActivePlayerTransform.position.y);

            // If it's the player's turn, handle player input for rotating the ball
            if (IsPlayerTurn)
            {
                // Check if the Space key is pressed to launch the ball
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Launch();
                }

                // Get horizontal input to rotate the ball left or right
                float horizontalInput = Input.GetAxis("Horizontal");
                if (horizontalInput != 0)
                {
                    // Rotate the ball smoothly based on the horizontal input, clamped between minAngle and maxAngle
                    transform.Rotate(0, 0, Mathf.Clamp(-horizontalInput * rotateSpeed * Time.deltaTime, minAngle, maxAngle));
                }
            }
            else
            {
                // If it's not the player's turn, rotate the ball towards the enemy's target angle
                if (transform.rotation.eulerAngles.z < 180f + EnemyTargetAngle)
                {
                    // Get the sign of the enemy target angle to determine the rotation direction
                    float sign = Mathf.Sign(EnemyTargetAngle);
                    // Rotate the ball smoothly towards the target angle
                    transform.Rotate(0, 0, sign * rotateSpeed * Time.deltaTime);
                }
                else
                {
                    // If the ball reaches the enemy's target angle, launch the ball
                    Launch();
                }
            }
        }
    }

    // Launch the ball
    public void Launch()
    {
        // Add a force to the ball's Rigidbody2D component to make it move forward
        rb.AddForce(transform.right * moveForce);
        // Set the game state to "playing"
        gameManager.isPlaying = true;
        // Deactivate the ball's child (visual) object to show only the ball's Rigidbody2D motion
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Handle collision with the left or right walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Left") || collision.gameObject.CompareTag("Right"))
        {
            // Call the GameManager's RoundEnd method to handle the end of the round
            gameManager.RoundEnd(collision.gameObject.CompareTag("Left"));
        }
    }
}
