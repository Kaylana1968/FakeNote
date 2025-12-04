using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseContainer;
    [SerializeField] LevelLauncher levelLauncher;
    [SerializeField] GameObject number3;
    [SerializeField] GameObject number2;
    [SerializeField] GameObject number1;
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
        number3.SetActive(true);
        StartCoroutine(FirstResumeAfterDelay());
        
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
    private IEnumerator FirstResumeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        number3.SetActive(false);
        number2.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        number2.SetActive(false);
        number1.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1.0f;
        number1.SetActive(false);
        levelLauncher.ResumeMusic();

    }
    private IEnumerator WaitForReturn()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
}
