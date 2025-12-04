using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBlock : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text difficultyText;
    private Level level;
    public void Setup(LevelParameters levelBlock)
    {
        titleText.text = levelBlock.levelName ?? "Untitled";
        descriptionText.text = levelBlock.levelDescription ?? "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        difficultyText.text = "Difficulty : " + (levelBlock.levelDificulty ?? "1");
        level = levelBlock.level;
    }

    public void PlayLevel()
    {
        
        if (level != null) LevelSelection.SelectedLevel = level;
        if (PersistentManager.Instance != null)
        {
            PersistentManager.Instance.DestroySelf(); 
        }
        SceneManager.LoadScene("GameScene");
    }
}
