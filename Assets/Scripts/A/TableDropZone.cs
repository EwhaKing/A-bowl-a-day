
// Assets/Scripts/Interactable/TableDropZone.cs
using UnityEngine;

public class TableDropZone : MonoBehaviour
{
    void Start()
    {
        gameObject.tag = "DropZone";
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }
}