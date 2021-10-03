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

    public void StopPause()
    {
        Time.timeScale = 1f;
        GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        PlayerPrefs.SetInt("Paused", 0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EasyDifficulty()
    {
        PlayerPrefs.SetInt("MaxEnemyCount", 3);
        PlayerPrefs.SetInt("MaxTargetCount", 2);
    }

    public void ModerateDifficulty()
    {
        PlayerPrefs.SetInt("MaxEnemyCount", 5);
        PlayerPrefs.SetInt("MaxTargetCount", 3);
    }

    public void HardDifficulty()
    {
        PlayerPrefs.SetInt("MaxEnemyCount", 7);
        PlayerPrefs.SetInt("MaxTargetCount", 4);
    }

    public void LoadARTest()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ARSceneTest");
    }

    public void CanShoot()
    {
        PlayerPrefs.SetString("Shooting", "Yes");
    }
}
