using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour
{
    public OrderData[] availableOrders;
    public float spawnInterval = 10f;

    private OrderData currentOrder;

    public OrderData GetCurrentOrder()
    {
        return currentOrder;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCustomer()
    {
        if (availableOrders.Length == 0) return;
        currentOrder = availableOrders[Random.Range(0, availableOrders.Length)];
        Debug.Log("손님 등장! 주문: 떡볶이");
        // 2개 → 1개로 바꾸기
        DialogueUI.Instance.Show(currentOrder.customerDialogue, "Customer");
        Invoke("HideDialogue", 3f);

        StartCoroutine(CustomerTimer());
    }

    void HideDialogue()
    {
        DialogueUI.Instance.Hide();
    }

    IEnumerator CustomerTimer()
    {
        yield return new WaitForSeconds(currentOrder.timeLimit);
        Debug.Log("시간 초과! 손님 퇴장");
        currentOrder = null;
    }

    public void CustomerLeave(bool success)
    {
        StopAllCoroutines();
        currentOrder = null;
        Debug.Log(success ? "성공! 손님 만족" : "실패! 손님 불만족");
        StartCoroutine(SpawnRoutine());
    }
}