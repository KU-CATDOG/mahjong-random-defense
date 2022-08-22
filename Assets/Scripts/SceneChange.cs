using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneChangeGamePlay()
    {
        SceneManager.LoadScene("GameSceneRenewal");
    }

    public void SceneChangeTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
