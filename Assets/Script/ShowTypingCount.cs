using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTypingCount : MonoBehaviour
{
    public TextMeshProUGUI typingCountText;

    private int typingCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        typingCountText = GetComponent<TextMeshProUGUI>();

        typingCount = TypingManager.Instance.successCount + TypingManager.Instance.failureCount;

        ShowResults(typingCount);
    }

    void ShowResults(int typingCount)
    {
        typingCountText.text = "ëçë≈êî:" + typingCount.ToString();
    }
}
