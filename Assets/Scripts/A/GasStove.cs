using UnityEngine;

public class GasStove : MonoBehaviour
{
    public enum StoveState { Off, High, Low }
    public StoveState currentState = StoveState.Off;

    [Header("--- 불꽃 오브젝트 설정 ---")]
    public SpriteRenderer flameRenderer;
    public Sprite flameOff;
    public Sprite flameHigh;
    public Sprite flameLow;

    [Header("--- 가스레인지 본체/레버 설정 ---")]
    [HideInInspector] // 이제 인스펙터에서 드래그 안 해도 되므로 숨깁니다.
    public SpriteRenderer stoveRenderer;
    public Sprite stoveOff;
    public Sprite stoveHigh;
    public Sprite stoveLow;

    void Awake()
    {
        // [자동 할당] 스크립트가 붙어있는 오브젝트에서 SpriteRenderer를 자동으로 찾아옵니다.
        if (stoveRenderer == null)
        {
            stoveRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Start()
    {
        // 혹시나 flameRenderer도 비어있다면 자식 오브젝트에서 찾아주는 예외 처리
        if (flameRenderer == null)
        {
            flameRenderer = transform.Find("Flame_Sprite")?.GetComponent<SpriteRenderer>();
        }

        UpdateStoveVisual();
    }

    void OnMouseDown()
    {
        if (currentState == StoveState.Off)
            currentState = StoveState.High;
        else if (currentState == StoveState.High)
            currentState = StoveState.Low;
        else
            currentState = StoveState.Off;

        UpdateStoveVisual();
    }

    void UpdateStoveVisual()
    {
        // 안전 장치: 컴포넌트가 존재할 때만 이미지를 바꾸도록 체크
        if (flameRenderer != null)
        {
            switch (currentState)
            {
                case StoveState.Off: flameRenderer.sprite = flameOff; break;
                case StoveState.High: flameRenderer.sprite = flameHigh; break;
                case StoveState.Low: flameRenderer.sprite = flameLow; break;
            }
        }

        if (stoveRenderer != null)
        {
            switch (currentState)
            {
                case StoveState.Off: stoveRenderer.sprite = stoveOff; break;
                case StoveState.High: stoveRenderer.sprite = stoveHigh; break;
                case StoveState.Low: stoveRenderer.sprite = stoveLow; break;
            }
        }
        else
        {
            Debug.LogError($"{gameObject.name} 오브젝트에 SpriteRenderer 컴포넌트가 없습니다!");
        }
    }
}