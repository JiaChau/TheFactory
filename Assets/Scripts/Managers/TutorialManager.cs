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
