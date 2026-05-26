using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TutorialStepData[] steps;
    public DialogueUI dialogueUI;
    public HighlightController highlight;
    public RectTransform fingerIcon;

    private int currentStep = 0;

    void Start() { ShowStep(currentStep); }

    void ShowStep(int idx)
    {
        if (idx >= steps.Length)
        {
            dialogueUI.Hide();
            SceneManager.LoadScene("GameScene");
            return;
        }

        TutorialStepData step = steps[idx];
        dialogueUI.Show(step.dialogueText);
        highlight.SetTarget(step.highlightTarget);
        fingerIcon.anchoredPosition = step.fingerPosition;
        fingerIcon.gameObject.SetActive(step.fingerPosition != Vector2.zero);
    }

    public void NextStep()
    {
        currentStep++;
        ShowStep(currentStep);
    }
}