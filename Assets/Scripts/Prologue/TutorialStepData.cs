using UnityEngine;

[CreateAssetMenu(menuName = "KingBuk/TutorialStep")]
public class TutorialStepData : ScriptableObject
{
    [TextArea]
    public string dialogueText;
    public GameObject highlightTarget;
    public Vector2 fingerPosition;
    public bool waitForClick;
}