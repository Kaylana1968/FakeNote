using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseContainer;
    [SerializeField] LevelLauncher levelLauncher;
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
                pauseContainer.SetActive(true);
                Time.timeScale = 0f;

                levelLauncher.PauseMusic();
        }
    }


    public void ResumeButton()
    {
        pauseContainer.SetActive(false);
        StartCoroutine(ResumeAfterDelay());
        
    }

    public void RetryButton()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator ResumeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1.0f;
        levelLauncher.ResumeMusic();
    }
}
