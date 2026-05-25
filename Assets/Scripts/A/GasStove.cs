// Assets/Scripts/Interactable/GasStove.cs
using UnityEngine;

public class GasStove : MonoBehaviour
{
    [Header("불꽃 스프라이트 설정")]
    [SerializeField] private SpriteRenderer flameRenderer;
    [SerializeField] private Sprite sprFlameOff;
    [SerializeField] private Sprite sprFlameHigh;
    [SerializeField] private Sprite sprFlameLow;

    [SerializeField] private GameObject potObject; // Pot_Root 연동

    private int _clickCount = 0;

    void Start()
    {
        flameRenderer.sprite = sprFlameOff;
    }

    // 마우스로 가스버너 오브젝트 자체를 클릭할 때 작동
    void OnMouseDown()
    {
        // 1. 첫 번째 클릭: 불 켜기 (Idle 상태일 때만 가능)
        if (_clickCount == 0 && GameManager.Instance.CurrentState == GameState.Idle)
        {
            _clickCount = 1;
            flameRenderer.sprite = sprFlameHigh;
            potObject.SetActive(true); // 냄비 등장
            GameManager.Instance.SetState(GameState.GasOn_High);
        }
        // 2. 두 번째 클릭: 불 줄이기 (조리 시작된 강불 상태에서만 가능)
        else if (_clickCount == 1 && GameManager.Instance.CurrentState == GameState.Cooking_HighFlame)
        {
            _clickCount = 2;
            flameRenderer.sprite = sprFlameLow;
            GameManager.Instance.TimeSinceLastAction = 0f; // 타는 타이머 초기화
            GameManager.Instance.SetState(GameState.Cooking_LowFlame);
        }
    }
}
