using UnityEngine;
using UnityEngine.SceneManagement;
public class ToSettingSceneButton : MonoBehaviour
{
    public void OnClickToSettingSceneButton()
    {
        SceneManager.LoadScene("ShowRankScene");
    }
}