// Assets/Scripts/Interactable/Pot.cs
using UnityEngine;

public class Pot : MonoBehaviour
{
    public static Pot Instance { get; private set; }

    [Header("냄비 상태별 스프라이트")]
    [SerializeField] private SpriteRenderer potRenderer;
    [SerializeField] private Sprite sprEmpty;
    [SerializeField] private Sprite sprWithWater;
    [SerializeField] private Sprite sprCooking;
    [SerializeField] private Sprite sprBurnt;

    [SerializeField] private Transform targetStovePos;
    private bool _isPlaced = false;

    void Awake()
    {
        Instance = this;
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.CurrentState == GameState.GasOn_High && !_isPlaced)
        {
            _isPlaced = true;
            transform.position = new Vector3(targetStovePos.position.x, targetStovePos.position.y, 0f);
            GameManager.Instance.SetState(GameState.PotPlaced);
        }
    }

    public void SetWaterVisual() => potRenderer.sprite = sprWithWater;
    public void SetCookingVisual() => potRenderer.sprite = sprCooking;
    public void SetBurntVisual() => potRenderer.sprite = sprBurnt;
}