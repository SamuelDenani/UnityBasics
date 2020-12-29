using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Transform GroundCheckTransform;
    [SerializeField] LayerMask playerMask;

    Rigidbody rb;

    int superJumpsRemaining;
    bool jumpKeyWasPressed;
    float horizontalInput;
    bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        isGrounded = Physics.CheckSphere(GroundCheckTransform.position, 0.1f, playerMask);

        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpKeyWasPressed = Input.GetKeyDown(KeyCode.Space);
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {
        
        if (jumpKeyWasPressed && isGrounded) {
            float jumpForce = 270f;

            if (superJumpsRemaining > 0) {
                jumpForce *= 3;
                superJumpsRemaining--;
            }
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

        rb.velocity = new Vector3(horizontalInput, rb.velocity.y, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 9) {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }
}