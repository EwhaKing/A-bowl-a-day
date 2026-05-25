using System;
using UnityEngine;

public class JudgeSystem : MonoBehaviour
{
    public static JudgeSystem Instance;

    public event Action<bool> OnJudgeResult;

    void Awake()
    {
        Instance = this;
    }

    public void HandleResult(bool success)
    {
        if (success)
        {
            RevenueManager.Instance.SuccessOrder(3000);
            AudioManager.Instance.PlaySuccess();
        }
        else
        {
            RevenueManager.Instance.FailOrder(1500);
            AudioManager.Instance.PlayFail();
        }

        OnJudgeResult?.Invoke(success);

        Debug.Log(
            success ? "성공 판정" : "실패 판정"
        );
    }
}