using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerPrefs.SetInt("Tutorial Level", 0);
        PlayerPrefs.SetInt("DarkMode", 1);
    }

    public void LoadLevelHub()
    {
        SceneManager.LoadScene("LevelHub");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("OptionMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
