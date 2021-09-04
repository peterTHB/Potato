using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("SettingsScene");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
