///<summary>
///This is a "look around" script
///Handles the camera movement
///</summary>
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float clamp = 30;
    //this could potentially be taken away (see below for how)
    public GameObject head;
    private float xRotation = 0f;
    private float yRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yRotation = 180;

    }

    void Update()
    {
        if (!CanvasManagers.Instance.isInventoryOpen)
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
            //this could be changed to:
            //Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            //AS LONG AS the camera is in the same place 
            head.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }

    }
}
