using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DaySummaryUI : MonoBehaviour
{
    public TextMeshProUGUI receiptText;

    void Start()
    {
        int revenue =
            PlayerPrefs.GetInt("Revenue", 0);

        int success =
            PlayerPrefs.GetInt("Success", 0);

        int fail =
            PlayerPrefs.GetInt("Fail", 0);

        receiptText.text =
            "━━━━━━━━━━━━━━\n" +
            " DAILY REPORT\n" +
            "━━━━━━━━━━━━━━\n\n" +
            $"Success   {success}\n" +
            $"Fail      {fail}\n\n" +
            "━━━━━━━━━━━━━━\n" +
            $"Revenue   {revenue} G\n" +
            "━━━━━━━━━━━━━━";
    }

    public void OnNextDayClick()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(
            "GameScene"
        );
    }
}