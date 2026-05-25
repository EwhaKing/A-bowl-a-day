// Assets/Scripts/Interactable/DraggableObject.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableObject : MonoBehaviour
{
    public bool CanDrag = true;
    public float ReturnSpeed = 10f;

    protected Vector3 StartPos;
    private Vector3 _offset;
    private bool _isDragging;
    private Camera _cam;

    protected virtual void Start()
    {
        // Z축은 무조건 0으로 고정하여 증발 현상 방지
        StartPos = new Vector3(transform.position.x, transform.position.y, 0f);
        _cam = Camera.main;
    }

    void OnMouseDown()
    {
        if (!CanDrag) return;
        _isDragging = true;
        _offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (!_isDragging) return;
        Vector3 nextPos = GetMouseWorldPos() + _offset;
        nextPos.z = 0f; // Z값 강제 고정
        transform.position = nextPos;
        WhileDragging();
    }

    void OnMouseUp()
    {
        if (!_isDragging) return;
        _isDragging = false;
        OnReleased();
    }

    protected virtual void WhileDragging() { }
    protected virtual void OnReleased() => ReturnToStart();

    protected void ReturnToStart()
    {
        StopAllCoroutines();
        StartCoroutine(MoveBack(StartPos));
    }

    protected System.Collections.IEnumerator MoveBack(Vector3 target)
    {
        target.z = 0f;
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, ReturnSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Mathf.Abs(_cam.transform.position.z);
        return _cam.ScreenToWorldPoint(mousePos);
    }
}