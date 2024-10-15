using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves.
    public float speed = 0;

    // Set the count in the UI
    public TextMeshProUGUI countText;

    // Set the win text in the UI
    public TextMeshProUGUI winText;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    private int count;

    // Rigidbody of the player.
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        //Initialize the count variable
        count = 0;

        // Display the current starting count value in the UI
        SetCountText();

        // Set the win text to to false at start.
        winText.gameObject.SetActive(false);
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    // This function is called when a move input is detected
    private void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if ( count >= 8)
        {
            winText.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("Pickup"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Add 1 to the count variable
            count = count + 1;

            // Update the UI with the new count value
            SetCountText();
        }
    }
}
