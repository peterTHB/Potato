using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMusic>().StopMusic();

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
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMusic>().PlayingMusic();
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

        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMusic>().PlayingMusic();

        SceneManager.LoadScene("SettingsScene");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<PlayMusic>().PlayingMusic();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }

    public void StopPause()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Paused", 0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EasyDifficulty()
    {
        PlayerPrefs.SetString("DifficultyText", "Easy");
        PlayerPrefs.SetInt("MaxEnemyCount", 3);
        PlayerPrefs.SetInt("MaxTargetCount", 1);
        PlayerPrefs.SetFloat("RotateAroundSpeed", 20f);
        PlayerPrefs.SetFloat("MoveSpeed", 3f);
        PlayerPrefs.SetFloat("RotationSpeed", 40f);
        PlayerPrefs.SetFloat("DrillMoveSpeed", 2f);
    }

    public void ModerateDifficulty()
    {
        PlayerPrefs.SetString("DifficultyText", "Moderate");
        PlayerPrefs.SetInt("MaxEnemyCount", 5);
        PlayerPrefs.SetInt("MaxTargetCount", 2);
        PlayerPrefs.SetFloat("RotateAroundSpeed", 30f);
        PlayerPrefs.SetFloat("MoveSpeed", 4f);
        PlayerPrefs.SetFloat("RotationSpeed", 50f);
        PlayerPrefs.SetFloat("DrillMoveSpeed", 3f);
    }

    public void HardDifficulty()
    {
        PlayerPrefs.SetString("DifficultyText", "Hard");
        PlayerPrefs.SetInt("MaxEnemyCount", 7);
        PlayerPrefs.SetInt("MaxTargetCount", 3);
        PlayerPrefs.SetFloat("RotateAroundSpeed", 40f);
        PlayerPrefs.SetFloat("MoveSpeed", 5f);
        PlayerPrefs.SetFloat("RotationSpeed", 60f);
        PlayerPrefs.SetFloat("DrillMoveSpeed", 4f);
    }

    public void CanShoot()
    {
        GameObject.FindGameObjectWithTag("FireButton").GetComponent<AudioSource>().Play();
        PlayerPrefs.SetString("Shooting", "Yes");
    }

    public void PlayerPausing()
    {
        if (PlayerPrefs.GetInt("Paused") == 1)
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("Paused", 0);
        }
        else if (PlayerPrefs.GetInt("Paused") == 0)
        {
            Time.timeScale = 0f;
            PlayerPrefs.SetInt("Paused", 1);
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
