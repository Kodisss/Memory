using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdownMenu;

    private void Start()
    {
        dropdownMenu.value = PlayerPrefs.GetInt("Difficulty");
    }

    private void Update()
    {
        ManageInputs();
    }

    public void SetDifficulty(int input)
    {
        // Save the difficulty setting
        PlayerPrefs.SetInt("Difficulty", input);
        PlayerPrefs.Save();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ManageInputs()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            LoadMainMenu();
        }
    }
}
