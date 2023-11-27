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
        // �V�[�����ǂݍ��܂ꂽ�Ƃ��Ɏ��s�����������������ɒǉ�
        DatabaseManager.Instance.AddResult(2000,20, 90.0f,30);
    }
}
