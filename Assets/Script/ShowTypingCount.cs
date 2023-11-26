using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTypingCount : MonoBehaviour
{
    public TextMeshProUGUI typingCountText;

    private int typingCount = 110;
    // Start is called before the first frame update
    void Start()
    {
        typingCountText = GetComponent<TextMeshProUGUI>();

        ShowResults(typingCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowResults(int typingCount)
    {
        typingCountText.text = "ëçÉ^ÉCÉvêî:" + typingCount.ToString();
    }
}
