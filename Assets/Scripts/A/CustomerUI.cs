using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    public CustomerManager customerManager;
    public TextMeshProUGUI orderText;
    public Image timerBar;

    private float elapsed;

    void Update()
    {
        OrderData order = customerManager.GetCurrentOrder();

        if (order != null)
        {
            orderText.text = "Order: " + order.dishName;
            elapsed += Time.deltaTime;
            timerBar.fillAmount = 1f - (elapsed / order.timeLimit);
        }
        else
        {
            orderText.text = "";
            elapsed = 0f;
            timerBar.fillAmount = 1f;
        }
    }
}