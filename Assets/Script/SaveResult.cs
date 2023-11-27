using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveResult : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ƒV[ƒ“‚ª“Ç‚İ‚Ü‚ê‚½‚Æ‚«‚ÉÀs‚µ‚½‚¢ˆ—‚ğ‚±‚±‚É’Ç‰Á
        DatabaseManager.Instance.AddResult(2000,20, 90.0f,30);
    }
}
