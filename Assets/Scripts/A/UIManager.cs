// Assets/Scripts/UI/UIManager.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("텍스트 패널 인터페이스")]
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private TextMeshProUGUI ingredientStatusText;

    [Header("게이지 바")]
    [SerializeField] private GameObject boilPanel;
    [SerializeField] private Image boilBarFill;

    [Header("결과 오버레이")]
    [SerializeField] private GameObject resultScreen;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button btnRetry;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        boilPanel.SetActive(false);
        resultScreen.SetActive(false);
        btnRetry.onClick.AddListener(() => GameManager.Instance.Restart());
        UpdateIngredientStatus();
        ShowHint("가스버너를 한 번 클릭해서 불을 켜세요!");
    }

    public void UpdateBoilBar(float fillRatio)
    {
        if (boilBarFill != null) boilBarFill.fillAmount = Mathf.Clamp01(fillRatio);
    }

    public void ShowHint(string text) => hintText.text = text;

    public void UpdateIngredientStatus()
    {
        if(GameManager.Instance == null) return;
        var gm = GameManager.Instance;
        string status = "필요 재료: ";
        status += gm.SauceAdded ? "<color=green>소스✓</color> " : "소스 ";
        status += gm.TteokAdded ? "<color=green>떡✓</color> " : "떡 ";
        status += gm.EomukAdded ? "<color=green>어묵✓</color> " : "어묵 ";
        status += gm.PaAdded ? "<color=green>파✓</color> " : "파 ";
        status += gm.YangbaechuAdded ? "<color=green>양배추✓</color>" : "양배추";
        ingredientStatusText.text = status;
    }

    public void OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GasOn_High:
                ShowHint("빈 냄비를 클릭하여 가스버너 위에 올리세요!");
                break;
            case GameState.PotPlaced:
                ShowHint("물통을 드래그해서 냄비에 물을 부으세요!");
                break;
            case GameState.WaterAdded:
            case GameState.IngredientsAdding:
                ShowHint("소스, 떡, 어묵, 파, 양배추 재료들을 모두 냄비로 드래그해 넣으세요!");
                break;
            case GameState.Cooking_HighFlame:
                ShowHint("🔥 위험! 불이 너무 강합니다! 가스버너를 한 번 더 클릭해서 불을 줄이세요!");
                break;
            case GameState.Cooking_LowFlame:
                boilPanel.SetActive(true);
                ShowHint("주걱을 드래그하여 냄비 속을 계속 섞어주세요! 안 섞으면 금방 탑니다!");
                break;
            case GameState.CookDone:
                boilPanel.SetActive(false);
                ShowHint("✨ 요리 완성! 접시를 드래그해서 떡볶이를 담으세요!");
                break;
            case GameState.Plating:
                ShowHint("떡볶이가 담긴 접시를 우측 식탁(DropZone) 구역으로 배달하세요!");
                break;
        }
    }

    public void ShowResult(CookResult result)
    {
        resultScreen.SetActive(true);
        if (result == CookResult.Perfect) resultText.text = "🏆 성공! 맛있는 떡볶이를 대접했습니다!";
        else resultText.text = "🔥 실패! 떡볶이가 새까맣게 타버렸습니다...";
    }
}