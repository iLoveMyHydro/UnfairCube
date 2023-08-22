using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Das Main Menu wurde durch folgende Quellen erstellt: 
    //https://youtu.be/zc8ac_qUXQY
    //https://youtu.be/IuuKUaZQiSU

    #region Parameter

    #region Const

    private const string LevelHubSceneText = "LevelHub";

    private const string OptionMenuSceneText = "OptionMenu";

    #endregion

    #endregion


    #region Start

    /// <summary>
    /// The mouse will be locked and set to visible
    /// Also the darkMode Pref and the Tutorial Level Pref will be set 
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #endregion

    #region LoadLevelHub

    /// <summary>
    /// If you press the Play Button the Levelhub scene will be loaded
    /// </summary>
    public void LoadLevelHub()
    {
        SceneManager.LoadScene(LevelHubSceneText);
    }

    #endregion

    #region LoadOptions

    /// <summary>
    /// If you press the Options Button the Option Scene will be loaded 
    /// </summary>
    public void LoadOptions()
    {
        SceneManager.LoadScene(OptionMenuSceneText);
    }

    #endregion

    #region QuitGame

    /// <summary>
    /// If you press the Quit button the game will be closed or quit
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}
