using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
	[SerializeField] TMP_Text FinalScoreText;
	[SerializeField] TMP_Text PerfectText;
	[SerializeField] TMP_Text GoodText;
	[SerializeField] TMP_Text MissText;
	[SerializeField] TMP_Text BestComboText;
	[SerializeField] GameObject container;

	public void DisplayEndMenu(int combo, int bestCombo, int score, int perfect, int good, int miss)
	{
		if (combo > bestCombo)
		{
			bestCombo = combo;
		}

		FinalScoreText.text = "Score: " + score;
		PerfectText.text = "Perfect: " + perfect;
		GoodText.text = "Good: " + good;
		MissText.text = "Miss: " + miss;
		BestComboText.text = "Best Combo: " + bestCombo;
		container.SetActive(true);
		Time.timeScale = 0f;
	}

	public void RetryButton()
	{
		Time.timeScale = 1f;
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);
	}

	public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
