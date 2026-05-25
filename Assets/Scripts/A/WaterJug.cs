// Assets/Scripts/Interactable/WaterJug.cs
using UnityEngine;

public class WaterJug : DraggableObject
{
    [SerializeField] private float snapDistance = 1.5f;
    private Transform _potTransform;

    protected override void Start()
    {
        base.Start();
        _potTransform = GameObject.FindWithTag("Pot").transform;
    }

    protected override void OnReleased()
    {
        float dist = Vector3.Distance(transform.position, _potTransform.position);

        if (dist < snapDistance && GameManager.Instance.CurrentState == GameState.PotPlaced)
        {
            StartCoroutine(PourRoutine());
        }
        else
        {
            ReturnToStart();
        }
    }

    System.Collections.IEnumerator PourRoutine()
    {
        CanDrag = false;
        yield return MoveBack(_potTransform.position + Vector3.up * 1.2f);

        // 물 붓는 회전 연출
        float t = 0f;
        while (t < 0.4f)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, -65f, t / 0.4f));
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        transform.rotation = Quaternion.identity;

        Pot.Instance.SetWaterVisual();
        GameManager.Instance.WaterAdded = true;
        GameManager.Instance.SetState(GameState.WaterAdded);

        ReturnToStart();
    }
}