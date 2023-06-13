using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void IAGame()
    {
        // Save the difficulty setting
        PlayerPrefs.SetString("GameMode", "IA");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Connect4");
    }

    public void PVPGame()
    {
        // Save the difficulty setting
        PlayerPrefs.SetString("GameMode", "PVP");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Connect4");
    }

    public void SettingsScreen()
    {
        SceneManager.LoadScene("SettingsScreen");
    }
}
