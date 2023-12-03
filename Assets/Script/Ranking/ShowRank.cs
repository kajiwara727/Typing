using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SQLite4Unity3d;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

public class TypingResultsDisplay : MonoBehaviour
{
    public Transform panel; // PanelのTransformをアタッチ
    public TextMeshProUGUI rankingTextPrefab;
    public TextMeshProUGUI pointTextPrefab;
    public TextMeshProUGUI countTextPrefab;
    public TextMeshProUGUI accuracyTextPrehab;
    public TextMeshProUGUI speedTextPrefab;

    public int fixedColumnCount = 5;

    void Start()
    {
        // データベースからTypingResultデータを取得
        List<TypingResult> typingResults = DatabaseManager.Instance.GetTypingResultsOrderedByPoint();

        int rank = 1;

        // UI要素の動的生成
        foreach (var result in typingResults)
        {
            if(rank > 5)
            {
                break;
            }

            TextMeshProUGUI rankText = Instantiate(rankingTextPrefab, panel);
            rankText.text = $"{rank}";

            TextMeshProUGUI pointText = Instantiate(pointTextPrefab, panel);
            pointText.text = $"{result.Point}";

            TextMeshProUGUI countText = Instantiate(countTextPrefab, panel);
            countText.text = $"{result.TypingCount}";

            TextMeshProUGUI accuracyText = Instantiate(accuracyTextPrehab, panel);
            accuracyText.text = $"{result.Accuracy}";

            TextMeshProUGUI speedText = Instantiate(speedTextPrefab, panel);
            speedText.text = $"{result.Speed}";

            rank++;
        }
    }
}