// Assets/Scripts/Interactable/Spatula.cs
using UnityEngine;

public class Spatula : DraggableObject
{
    [SerializeField] private float stirRadius = 2.2f;
    private Transform _potTransform;
    private Vector3 _lastFramePos;

    protected override void Start()
    {
        base.Start();
        _potTransform = GameObject.FindWithTag("Pot").transform;
    }

    protected override void WhileDragging()
    {
        if (GameManager.Instance.CurrentState != GameState.Cooking_LowFlame) return;

        float dist = Vector3.Distance(transform.position, _potTransform.position);
        if (dist <= stirRadius)
        {
            // 이전 프레임 위치와 마우스 위치 간 격차가 클 때 (즉, 마우스를 흔들 때만 젓는 걸로 인정)
            float moveDist = Vector3.Distance(transform.position, _lastFramePos);
            if (moveDist > 0.03f)
            {
                GameManager.Instance.OnSpatulaStir();

                // 주걱 각도 흔들리는 효과
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 20f) * 15f);
            }
        }
        _lastFramePos = transform.position;
    }

    protected override void OnReleased()
    {
        transform.rotation = Quaternion.identity;
        ReturnToStart();
    }
}
