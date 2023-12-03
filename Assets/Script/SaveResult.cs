using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveResult : MonoBehaviour
{
    private int success;
    private int failure;
    private int point;

    private float accuracy;
    private int typeCount;
    private float speed;


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
        success = TypingManager.Instance.successCount;
        failure = TypingManager.Instance.failureCount;
        typeCount = success + failure;

        accuracy = (float)success / (float)typeCount;

        point = success * 10 - failure * 5;
        speed = (float)typeCount / 60;
        speed = Mathf.Round(speed * 100) / 100;
        
        // �V�[�����ǂݍ��܂ꂽ�Ƃ��Ɏ��s�����������������ɒǉ�
        DatabaseManager.Instance.AddResult(point,typeCount,accuracy,speed);
    }
}
