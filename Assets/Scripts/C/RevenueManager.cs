using UnityEngine;

public class RevenueManager : MonoBehaviour
{
    public static RevenueManager Instance;

    public int totalRevenue;
    public int successCount;
    public int failCount;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SuccessOrder(int money)
    {
        successCount++;
        totalRevenue += money;
    }

    public void FailOrder(int penalty)
    {
        failCount++;
        totalRevenue -= penalty;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(
            "Revenue",
            totalRevenue
        );

        PlayerPrefs.SetInt(
            "Success",
            successCount
        );

        PlayerPrefs.SetInt(
            "Fail",
            failCount
        );

        PlayerPrefs.Save();
    }
}