///<summary>
///This is the beginning of the "helper text" or "interact text"
///in the middle of the screen.
///with functions to turn it on and pass in a string to what you want it to say
///and to turn it off, it will become more useful
///</summary>
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    TMP_Text tutorialText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        canvas.SetActive(false);
    }

    public void SetTutorialCanvas(string text)
    {
        canvas.SetActive(true);
        tutorialText.text = text;
        
    }

    public void TurnOffTutorialCanvas()
    {
        tutorialText.text = "";
        canvas.SetActive(false);
    }
}
