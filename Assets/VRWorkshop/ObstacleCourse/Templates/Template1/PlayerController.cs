using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Speed of the sphere
    public float jumpForce = 5f; // Force applied for jumping

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Get input from the user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a vector for movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply the movement to the Rigidbody
        rb.AddForce(movement * speed);

        // Jump if the spacebar is pressed and the sphere is grounded
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump!");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

