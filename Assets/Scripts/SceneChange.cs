using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneChangeGamePlay()
    {
        SceneManager.LoadScene("GameScene");
    }
}
