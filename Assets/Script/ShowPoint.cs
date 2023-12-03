using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;


public class ShowPoint : MonoBehaviour
{
    private TypingManager typingManager;

    public TextMeshProUGUI pointText;

    private int successCount;
    private int failureCount;
    private int point;

    // Start is called before the first frame update
    void Start()
    {
        successCount = TypingManager.Instance.successCount;
        failureCount = TypingManager.Instance.failureCount;
        point = CalculatePoint(successCount, failureCount);

        pointText = GetComponent<TextMeshProUGUI>();

        GameObject typingManagerObject = GameObject.Find("Typing");

        ShowResults(point);
    }

    void ShowResults(int point)
    {
        pointText.text = "ì_êî:" + point.ToString();
    }

    int CalculatePoint(int successCount, int failureCount)
    {
        int point = successCount * 10 - failureCount * 5;

        return point;
    }
}
