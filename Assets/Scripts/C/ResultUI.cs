using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    void Start()
    {
        JudgeSystem.Instance.OnJudgeResult += ShowResult;
        resultPanel.SetActive(false);
    }

    void ShowResult(bool success)
    {
        int price = success ? 3000 : 1500;

        resultPanel.SetActive(true);

        if (success)
            resultText.text =
                $"<color=#7FFF7F>✓ 성공! +{price}원</color>";
        else
            resultText.text =
                $"<color=#FF7F7F>✗ 실패... -{price}원</color>";

        Invoke("Hide", 2f);
    }

    void Hide()
    {
        resultPanel.SetActive(false);
    }
}