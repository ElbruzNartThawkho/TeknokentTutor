using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOtherScene : MonoBehaviour
{
    public void OtherScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
