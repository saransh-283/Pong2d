using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // The speed at which the player can move up and down
    public float minY = -3.5f;  // The minimum Y position the player can move to
    public float maxY = 3.5f;   // The maximum Y position the player can move to

    private void Update()
    {
        // Check if the "W" key is pressed (move up)
        if (Input.GetKey(KeyCode.W))
        {
            // Move the player up while clamping the Y position within the valid range
            // The movement is based on the player's current Y position, the speed, and Time.deltaTime
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y + speed * Time.deltaTime, minY, maxY));
        }

        // Check if the "S" key is pressed (move down)
        if (Input.GetKey(KeyCode.S))
        {
            // Move the player down while clamping the Y position within the valid range
            // The movement is based on the player's current Y position, the speed, and Time.deltaTime
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y - speed * Time.deltaTime, minY, maxY));
        }
    }
}
