using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToTopScene : MonoBehaviour
{
    public void OnClickToTopSceneButton()
    {
        SceneManager.LoadScene("TopScene");
    }
}