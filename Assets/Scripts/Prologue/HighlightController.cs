using UnityEngine;
using UnityEngine.UI;

public class HighlightController : MonoBehaviour
{
    public Image overlay;  // 어두운 배경

    public void SetTarget(GameObject target)
    {
        if (target == null)
        {
            overlay.gameObject.SetActive(false);
        }
        else
        {
            overlay.gameObject.SetActive(true);
        }
    }
}
