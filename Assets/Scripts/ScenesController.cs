using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesController : MonoBehaviour
{

    /// <summary>
    /// 載入場景(場景名)
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //離開遊戲
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
