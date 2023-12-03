using UnityEngine;

public class CanvasDrawer : MonoBehaviour
{
    public Canvas yourCanvas;  // InspectorでCanvasをアタッチする

    void Start()
    {
        Debug.Log("CanvasDrawer script started!");
        // ゲームシーンが移行したらCanvasを表示する
        ShowCanvas();
    }

    void ShowCanvas()
    {
        // Canvasをアクティブにする（表示する）
        yourCanvas.gameObject.SetActive(true);
    }
}