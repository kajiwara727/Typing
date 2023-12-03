using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTypingSpeed : MonoBehaviour
{
    public TextMeshProUGUI typingSpeedText;

    private int typingCount;

    private const float TIME = 60;

    // Start is called before the first frame update
    void Start()
    {
        typingCount = TypingManager.Instance.successCount + TypingManager.Instance.failureCount;
        typingSpeedText = GetComponent<TextMeshProUGUI>();

        ShowResults(typingCount);
    }

    float CalculateSpeed(int typingCount)
    {
        float typengSpeed = (float)typingCount / TIME;

        return typengSpeed;
    }

    void ShowResults(int typingCount)
    {
        float typingSpeed = CalculateSpeed(typingCount);
        typingSpeedText.text = "1ïbÇ†ÇΩÇËÇÃÉ^ÉCÉvêî:" + typingSpeed.ToString("F2") + "âÒ";
    }
}
