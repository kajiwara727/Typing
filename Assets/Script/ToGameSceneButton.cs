using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameSceneButton : MonoBehaviour
{
    // DatabaseManagerへの参照
    public DatabaseManager databaseManager;

    private void Start()
    {
        // シーン内にDatabaseManagerが存在することを確認
        databaseManager = FindObjectOfType<DatabaseManager>();
    }

    public void OnClickToGameSceneButton()
    {
        // ボタンがクリックされたときにデータベースに接続
        if (databaseManager != null)
        {
            databaseManager.ConnectToDatabase();
        }

        // ゲームシーンへ遷移
        SceneManager.LoadScene("GameScene");
    }
}
