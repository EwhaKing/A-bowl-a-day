using UnityEngine;

[CreateAssetMenu(fileName = "NewOrder", menuName = "Game/OrderData")]
public class OrderData : ScriptableObject
{
    public string dishName;
    public float timeLimit = 30f;
}