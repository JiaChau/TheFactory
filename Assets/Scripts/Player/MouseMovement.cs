using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float clamp = 30;
    public GameObject head;


    private float xRotation = 0f;
    private float yRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yRotation = 180;

    }

    // Update is called once per frame
    void Update()
    {
        // Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotate around the X axis (look up and down)
        xRotation -= mouseY;


        //clamp the rotation
        xRotation = Mathf.Clamp(xRotation, -clamp, clamp);

        //Rotate around the Y axis (look left and right)
        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        head.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, head.transform.position);
    }
}
