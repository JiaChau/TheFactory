using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public float openAngle = 130f;
    public float openDuration = 1.5f;
    public Transform doorOne;
    public Transform doorTwo;
    private bool playerInRange = false;
    private bool doorOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doorOpened)
        {
            TutorialManager.Instance.SetTutorialCanvas("E to Interact");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.Instance.TurnOffTutorialCanvas();
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (!playerInRange || doorOpened)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TutorialManager.Instance.TurnOffTutorialCanvas();
            StartCoroutine(OpenDoors());
        }
    }

    private IEnumerator OpenDoors()
    {
        doorOpened = true;

        Destroy(GetComponent<Collider>());

        Coroutine d1 = StartCoroutine(OpenDoor(doorOne, openAngle));
        Coroutine d2 = StartCoroutine(OpenDoor(doorTwo, -openAngle));

        // Wait for both coroutines to finish
        yield return d1;
        yield return d2;

        // Then destroy this script
        Destroy(this);
    }

    private IEnumerator OpenDoor(Transform door, float desiredAngle)
    {
        float elapsed = 0f;
        Quaternion startRot = door.rotation;
        Quaternion endRot = Quaternion.Euler(door.eulerAngles.x, door.eulerAngles.y + desiredAngle, door.eulerAngles.z);

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            door.rotation = Quaternion.Slerp(startRot, endRot, elapsed / openDuration);
            yield return null;
        }

        door.rotation = endRot;
    }
}
