using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToSettingSceneButton : MonoBehaviour
{
    public void OnClickToSettingSceneButton()
    {
        SceneManager.LoadScene("SettingScene");
    }
}