using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBlock : MonoBehaviour
{
    public Text titleText;
    private string sceneName;

    public void Setup(string sceneName)
    {
        this.sceneName = sceneName;
        titleText.text = sceneName;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
