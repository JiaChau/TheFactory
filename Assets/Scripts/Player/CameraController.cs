using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform headPivot;     // your head pivot
    public Camera cam;             // your Main Camera

    [Header("3P Settings")]
    public Vector3 thirdPersonOffset = new Vector3(0f, 1.5f, -3f);

    [Header("Look Settings")]
    public float mouseSensitivity = 100f;
    public float fpsClampAngle = 80f;   // max pitch for first person
    public float tpsClampAngle = 45f;   // max pitch for third person

    [Header("Start Mode")]
    public bool startFirstPerson = true;

    bool isFirstPerson;
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isFirstPerson = startFirstPerson;
        yRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (!CanvasManagers.Instance.isInventoryOpen)
        {
            // Toggle POV
            if (Input.GetKeyDown(KeyCode.V))
            isFirstPerson = !isFirstPerson;
        
            // Mouse look
            float mX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yRotation += mX;
            xRotation -= mY;

            // pick clamp based on mode
            float clampLimit = isFirstPerson ? fpsClampAngle : tpsClampAngle;
            xRotation = Mathf.Clamp(xRotation, -clampLimit, clampLimit);

            // apply rotations
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            headPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Position camera
            if (isFirstPerson)
            {
                cam.transform.position = headPivot.position;
                cam.transform.rotation = headPivot.rotation;
            }
            else
            {
                Quaternion camRot = Quaternion.Euler(xRotation, yRotation, 0f);
                Vector3 desiredPos = headPivot.position + camRot * thirdPersonOffset;
                cam.transform.position = desiredPos;
                cam.transform.LookAt(headPivot.position);
            }
        }
    }
}
