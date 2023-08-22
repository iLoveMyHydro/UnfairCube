using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Parameter

    #region Const

    private const string TutorialLevelPrefText = "Tutorial Level";

    private const string DarkModePrefText = "DarkMode";

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
        #region Fields 

        var tutorialNotPlayed = 0;

        var darkModeOn = 1;

        #endregion

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerPrefs.SetInt(TutorialLevelPrefText, tutorialNotPlayed);
        PlayerPrefs.SetInt(DarkModePrefText, darkModeOn);
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
