using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPoint : MonoBehaviour
{
    public TextMeshProUGUI pointText;

    private int successCount = 100;
    private int failureCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        pointText = GetComponent<TextMeshProUGUI>();

        ShowResults();
    }

    private void Update()
    {
        
    }

    void ShowResults()
    {
        int point = CalculatePoint(successCount, failureCount);

        pointText.text = "ì_êî:" + point.ToString();
    }

    int CalculatePoint(int successCount, int failureCount)
    {
        int point = successCount * 10 - failureCount * 5;

        return point;
    }
}
