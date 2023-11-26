using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameSceneButton : MonoBehaviour
{
    // DatabaseManager�ւ̎Q��
    public DatabaseManager databaseManager;

    private void Start()
    {
        // �V�[������DatabaseManager�����݂��邱�Ƃ��m�F
        databaseManager = FindObjectOfType<DatabaseManager>();
    }

    public void OnClickToGameSceneButton()
    {
        // �{�^�����N���b�N���ꂽ�Ƃ��Ƀf�[�^�x�[�X�ɐڑ�
        if (databaseManager != null)
        {
            databaseManager.ConnectToDatabase();
        }

        // �Q�[���V�[���֑J��
        SceneManager.LoadScene("GameScene");
    }
}
