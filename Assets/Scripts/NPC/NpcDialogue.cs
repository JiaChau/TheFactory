using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] TextAsset inkFile;
    [SerializeField] Animator emoteAnimator;

    //void OnEnable()  => GetComponent<DialogueTrigger>().Init(inkFile, emoteAnimator);
}
