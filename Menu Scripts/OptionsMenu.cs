using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //Das Option Menu wurde mit folgenden Quellen erstellt: 
    //https://youtu.be/YOaYQrN1oYQ

    #region Parameter

    #region Const 

    private const string MainMenuSceneText = "Main Menu";

    #endregion

    #region Not Sortable

    private bool vSync = true;

    private bool resetOn = true;

    private bool fullScreen = true;

    #endregion

    #endregion


    #region Load Menu

    /// <summary>
    /// If you press the Main Menu Button the MainMenu Scene will be loaded
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(MainMenuSceneText);
    }

    #endregion

    #region VSync

    /// <summary>
    /// If you set the toggle to on the VSync will be set on 
    /// If you set the toggle to off the VSync will be set off
    /// ONLY WORKS IN THE EXE !!!
    /// NOT IN UNITY EDITOR/UNITY ENGINE
    /// </summary>
    /// <param name="vSync"></param>
    public void VSync(bool vSync)
    {
        #region Fields

        var vSyncOn = 1;

        var vSyncOff = 0;

        #endregion

        if (vSync)
        {
            QualitySettings.vSyncCount = vSyncOn;
            this.vSync = false;
        }
        else
        {
            QualitySettings.vSyncCount = vSyncOff;
            this.vSync = true;
        }
    }

    #endregion

    #region Fullscreen

    /// <summary>
    /// If the toggle is set on the Application will be set to fullscreen
    /// If the toggle is set off the Application will be set to Windowed
    /// </summary>
    public void Fullscreen()
    {
        if (fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            fullScreen = false;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            fullScreen = true;
        }
    }

    #endregion

    #region Reset Tutorial 

    /// <summary>
    /// If you press the Reset Tutorial Level Button the prefs for the tutorial Level will be reseted
    /// </summary>
    public void ResetTutorialLevel()
    {
        #region Fields

        var didntPlayJumpTutorial = 0;

        #endregion

        if (resetOn)
        {
            PlayerPrefs.SetInt("First Jump", didntPlayJumpTutorial);

            PlayerPrefs.SetInt("Wall Jump", didntPlayJumpTutorial);
        }
    }

    #endregion
}