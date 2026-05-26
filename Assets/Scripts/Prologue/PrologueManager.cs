using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    public Image posterImage;      // 알바공고 이미지
    public GameObject clickHint;   // "클릭하여 계속" 텍스트
    private bool ready = false;

    void Start()
    {
        clickHint.SetActive(false);
        // 1초 후 힌트 표시
        Invoke("ShowHint", 1f);
    }

    void ShowHint() { clickHint.SetActive(true); ready = true; }

    void Update()
    {
        if (ready && Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Tutorial");
    }
}