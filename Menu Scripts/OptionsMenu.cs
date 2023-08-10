using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private bool vSyncOn = true;

    private bool fullScreen = true;

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void VSync()
    {
        var vSyncOffInt = -1;

        var vSyncOnInt = 30;

        if (vSyncOn)
        {
            Application.targetFrameRate = vSyncOffInt;
            vSyncOn = false;
        }
        else
        {
            Application.targetFrameRate = vSyncOnInt;
            vSyncOn = true;
        }
    }

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

    public void DarkMode()
    {
        SceneManager.LoadScene("OptionMenu");
    }
}