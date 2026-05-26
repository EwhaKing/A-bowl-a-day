using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerName;

    void Awake() { Instance = this; }

    public void Show(string text, string speaker = "Boss")
    {
        dialoguePanel.SetActive(true);
        speakerName.text = speaker;
        StopAllCoroutines();
        StartCoroutine(TypeText(text));
    }

    public void Hide() { dialoguePanel.SetActive(false); }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.04f);
        }
    }
}