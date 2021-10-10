using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (PlayerPrefs.GetString("DifficultyText").Equals(""))
        {
            ModerateDifficulty();
            PlayerPrefs.SetString("ViewingMode", "Normal");
        }
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
        if (PlayerPrefs.GetString("DifficultyText").Equals(""))
        {
            ModerateDifficulty();
            PlayerPrefs.SetString("ViewingMode", "Normal");
        }

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
        PlayerPrefs.SetInt("Paused", 0);
        //GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        //pauseMenu.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EasyDifficulty()
    {
        PlayerPrefs.SetString("DifficultyText", "Easy");
        PlayerPrefs.SetInt("MaxEnemyCount", 3);
        PlayerPrefs.SetInt("MaxTargetCount", 2);
    }

    public void ModerateDifficulty()
    {
        PlayerPrefs.SetString("DifficultyText", "Moderate");
        PlayerPrefs.SetInt("MaxEnemyCount", 5);
        PlayerPrefs.SetInt("MaxTargetCount", 3);
    }

    public void HardDifficulty()
    {
        PlayerPrefs.SetString("DifficultyText", "Hard");
        PlayerPrefs.SetInt("MaxEnemyCount", 7);
        PlayerPrefs.SetInt("MaxTargetCount", 4);
    }

    public void CanShoot()
    {
        //if (PlayerPrefs.GetInt("Paused") != 0)
        //{
        //    PlayerPrefs.SetString("Shooting", "Yes");
        //}
        PlayerPrefs.SetString("Shooting", "Yes");
    }

    public void PlayerPausing()
    {
        if (PlayerPrefs.GetInt("Paused") == 1)
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("Paused", 0);
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else if (PlayerPrefs.GetInt("Paused") == 0)
        {
            Time.timeScale = 0f;
            PlayerPrefs.SetInt("Paused", 1);
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetNormalView()
    {
        PlayerPrefs.SetString("ViewingMode", "Normal");
    }

    public void SetARView()
    {
        PlayerPrefs.SetString("ViewingMode", "AR");
    }
}
