using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public TextMeshProUGUI accuracyText;

    private int successCount;
    private int failureCount;
    private float accuracy;

    // Start is called before the first frame update
    void Start()
    {
        successCount = TypingManager.Instance.successCount;
        failureCount = TypingManager.Instance.failureCount;
        accuracy = CalculateAccuracy(successCount, failureCount);

        accuracyText = GetComponent<TextMeshProUGUI>();

        ShowResults(accuracy);
    }

    void ShowResults(float accuracy)
    {
        accuracyText.text = "ê≥ë≈ó¶: " + accuracy.ToString("F2") + "%";
    }

    float CalculateAccuracy(int successCount, int failureCount)
    {
        int totalTypes = successCount + failureCount;

        if(totalTypes == 0)
        {
            return 0f;
        }

        float accuracy = (float)successCount / totalTypes;
        return accuracy * 100f;
    }
}