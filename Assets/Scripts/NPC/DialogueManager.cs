using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] Button[] choiceBtns;

    public static DialogueManager I { get; private set; }
    public bool IsPlaying { get; private set; }

    Story story;

    void Awake() {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        panel.SetActive(false);
    }

    void Update() {
        if (!IsPlaying) return;
        //if (story.currentChoices.Count == 0 && InputManager.GetInstance().SubmitPressed())
            ContinueStory();
    }

    public void Enter(TextAsset inkJSON, Animator emoteAnim = null) {
        story      = new Story(inkJSON.text);
        IsPlaying  = true;
        panel.SetActive(true);
        ContinueStory();
    }

    void ContinueStory() {
        if (story.canContinue) {
            string line = story.Continue().Trim();
            ParseTags(story.currentTags);
            bodyText.text = line;
            ShowChoices();
        } else Exit();
    }

    void ShowChoices() {
        int i = 0;
        foreach (Choice c in story.currentChoices) {
            choiceBtns[i].gameObject.SetActive(true);
            choiceBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = c.text;
            int index = i;
            choiceBtns[i].onClick.RemoveAllListeners();
            choiceBtns[i].onClick.AddListener(() => { story.ChooseChoiceIndex(index); ContinueStory(); });
            i++;
        }
        for (; i < choiceBtns.Length; i++) choiceBtns[i].gameObject.SetActive(false);
    }

    void ParseTags(List<string> tags) {
        foreach (var tag in tags) {
            var parts = tag.Split(':');
            if (parts.Length != 2) continue;
            switch (parts[0].Trim().ToLower()) {
                case "speaker": nameText.text = parts[1].Trim(); break;
                case "emote":   /* 애니메이터 파라미터 처리 */    break;
            }
        }
    }

    void Exit() {
        IsPlaying = false;
        panel.SetActive(false);
        bodyText.text = nameText.text = "";
    }
}
