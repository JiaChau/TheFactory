using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;

    Rigidbody rb;
    bool isGrounded = true;
    [SerializeField]
    float jumpForce = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 camF = cameraTransform.forward; camF.y = 0; camF.Normalize();
        Vector3 camR = cameraTransform.right; camR.y = 0; camR.Normalize();

        Vector3 move = camF * inputZ + camR * inputX;
        rb.linearVelocity =move * moveSpeed;
        //if (move.sqrMagnitude > 1f) move.Normalize();

        //transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        Jump();
        if (CanvasManagers.Instance.isInventoryOpen)
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        else
        {
            //This DOESN'T MAKE SENSE
            //TO ADD TO CONSTRAINTS TOGETHER US THE | (OR)???? 
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX; ;
                        
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
