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
        // Save the gamemode settings
        PlayerPrefs.SetString("GameMode", "IA");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Memory");
    }

    public void PVPGame()
    {
        // Save the gamemode settings
        PlayerPrefs.SetString("GameMode", "PVP");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Memory");
    }

    public void SettingsScreen()
    {
        SceneManager.LoadScene("SettingsScreen");
    }
}
