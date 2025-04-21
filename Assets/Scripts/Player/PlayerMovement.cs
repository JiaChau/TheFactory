using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
   

    private void Update()
    {
        MoveForward();
        MoveSideways();
    }



    public void MoveForward()
    {
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * forwardInput * 5 * Time.deltaTime);
    }
    public void MoveSideways()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * 5 * Time.deltaTime);
    }


}
