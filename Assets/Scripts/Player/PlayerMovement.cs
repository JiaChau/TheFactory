using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 camF = cameraTransform.forward; camF.y = 0; camF.Normalize();
        Vector3 camR = cameraTransform.right; camR.y = 0; camR.Normalize();

        Vector3 move = camF * inputZ + camR * inputX;
        if (move.sqrMagnitude > 1f) move.Normalize();

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
