using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameSceneButton : MonoBehaviour
{
    public void OnClickToGameSceneButton()
    {

        // ゲームシーンへ遷移
        SceneManager.LoadScene("GameScene");
    }
}
