using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTypingSpeed : MonoBehaviour
{
    public TextMeshProUGUI typingSpeedText;

    private int typingCount = 110;

    private const float TIME = 60;

    // Start is called before the first frame update
    void Start()
    {
        typingSpeedText = GetComponent<TextMeshProUGUI>();

        ShowResults(typingCount);
    }

    // Update is called once per frame
    void Update()
    {
        
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
