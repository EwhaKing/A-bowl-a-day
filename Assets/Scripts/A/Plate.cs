// Assets/Scripts/Interactable/Plate.cs
using UnityEngine;

public class Plate : DraggableObject
{
    [SerializeField] private float snapDistance = 1.5f;
    [SerializeField] private SpriteRenderer plateDisplay;
    [SerializeField] private Sprite sprPlateWithFood;

    private Transform _potTransform;
    private bool _isPlated = false;

    protected override void Start()
    {
        base.Start();
        _potTransform = GameObject.FindWithTag("Pot").transform;
    }

    protected override void OnReleased()
    {
        if (!_isPlated)
        {
            float dist = Vector3.Distance(transform.position, _potTransform.position);
            if (dist < snapDistance && GameManager.Instance.CurrentState == GameState.CookDone)
            {
                _isPlated = true;
                plateDisplay.sprite = sprPlateWithFood;
                Pot.Instance.gameObject.SetActive(false); // 냄비 비우기
                GameManager.Instance.SetState(GameState.Plating);
                ReturnToStart();
            }
            else ReturnToStart();
        }
        else
        {
            ReturnToStart();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 식탁 구역(DropZone)에 접시를 배달하면 최종 성공
        if (_isPlated && other.CompareTag("DropZone"))
        {
            StopAllCoroutines();

            transform.position =
                other.transform.position;

            CanDrag = false;

            GameManager.Instance.SetState(
                GameState.Complete
            );

            // C 파트: 수익 처리
            RevenueManager.Instance.SuccessOrder(5000);

            UIManager.Instance.ShowResult(
                CookResult.Perfect
            );

            JudgeSystem.Instance.HandleResult(true);

            Debug.Log(
                "배달 성공 / 수익 +5000"
            );
        }
    }
}