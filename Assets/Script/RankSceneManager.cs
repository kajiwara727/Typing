using UnityEngine;

public class CanvasDrawer : MonoBehaviour
{
    public Canvas yourCanvas;  // Inspector��Canvas���A�^�b�`����

    void Start()
    {
        Debug.Log("CanvasDrawer script started!");
        // �Q�[���V�[�����ڍs������Canvas��\������
        ShowCanvas();
    }

    void ShowCanvas()
    {
        // Canvas���A�N�e�B�u�ɂ���i�\������j
        yourCanvas.gameObject.SetActive(true);
    }
}