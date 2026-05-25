// Assets/Scripts/Core/GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("현재 상태")]
    public GameState CurrentState = GameState.Idle;

    [Header("조리 밸런스 설정")]
    public float HighFlameBurnTime = 3f;  // 강불에서 불 안 줄이고 버틸 수 있는 시간 (초)
    public float LowFlameBurnTime = 5f;   // 약불에서 주걱 안 젓고 버틸 수 있는 시간 (초)
    public float TargetCookProgress = 10f; // 요리가 완성되기 위해 채워야 하는 총 게이지

    [HideInInspector] public float TimeSinceLastAction = 0f;
    [HideInInspector] public float CookProgress = 0f;
    [HideInInspector] public bool IsStirring = false;

    // 재료 체크용
    [HideInInspector] public bool WaterAdded, SauceAdded, TteokAdded, EomukAdded, PaAdded, YangbaechuAdded;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        // 1. 강불 조리 상태일 때 (재료 다 넣은 직후)
        if (CurrentState == GameState.Cooking_HighFlame)
        {
            TimeSinceLastAction += Time.deltaTime;
            // 강불인 상태로 시간이 초과되면 즉시 타버림!
            if (TimeSinceLastAction >= HighFlameBurnTime)
            {
                TriggerBurn();
            }
        }
        // 2. 약불 조리 상태일 때 (가스레인지 두 번 클릭해서 불 줄임)
        else if (CurrentState == GameState.Cooking_LowFlame)
        {
            if (!IsStirring)
            {
                TimeSinceLastAction += Time.deltaTime;
                if (TimeSinceLastAction >= LowFlameBurnTime)
                {
                    TriggerBurn();
                }
            }
            else
            {
                // 주걱을 젓고 있으면 타는 카운트가 초기화되고, 요리 게이지가 상승!
                TimeSinceLastAction = 0f;
                CookProgress += Time.deltaTime;

                // UI 게이지 바 업데이트
                UIManager.Instance.UpdateBoilBar(CookProgress / TargetCookProgress);

                if (CookProgress >= TargetCookProgress)
                {
                    SetState(GameState.CookDone);
                }
            }
        }
    }

    public void OnSpatulaStir()
    {
        if (CurrentState != GameState.Cooking_LowFlame) return;
        IsStirring = true;
        CancelInvoke(nameof(ResetStirFlag));
        Invoke(nameof(ResetStirFlag), 0.3f); // 0.3초 동안 마우스 입력 없으면 안 젓는 것으로 간주
    }

    void ResetStirFlag() => IsStirring = false;

    public bool AllIngredientsIn()
    {
        return WaterAdded && SauceAdded && TteokAdded && EomukAdded && PaAdded && YangbaechuAdded;
    }

    void TriggerBurn()
    {
        SetState(GameState.Failed);
        Pot.Instance.SetBurntVisual();
        UIManager.Instance.ShowResult(CookResult.Burnt);
    }

    public void SetState(GameState next)
    {
        CurrentState = next;
        UIManager.Instance.OnStateChanged(next);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void EndDay()
    {
        RevenueManager.Instance.SaveData();
        SceneManager.LoadScene("DaySummary");
    }
}