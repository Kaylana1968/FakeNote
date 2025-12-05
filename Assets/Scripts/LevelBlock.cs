using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelBlock : MonoBehaviour
{
	public TextMeshProUGUI titleText;
	public TextMeshProUGUI difficultyText;
	private Level level;
	public void Setup(LevelParameters levelBlock)
	{
		titleText.text = levelBlock.levelName ?? "Untitled";
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
