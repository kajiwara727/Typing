using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public TextMeshProUGUI accuracyText;

    private int successCount = 100;
    private int failureCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        accuracyText = GetComponent<TextMeshProUGUI>();

        ShowResults();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowResults()
    {
        float accuracy = CalculateAccuracy(successCount, failureCount);
        accuracyText.text = "ê≥ämÇ≥: " + accuracy.ToString("F2") + "%";
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