using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHubMenu : MonoBehaviour
{
    private int tutorialLevelPlayed = 0;

    public void TutorialLevel()
    {
        SceneManager.LoadScene("Tutorial Level");
    }

    public void FirstLevel()
    {
        var notPlayedTutorial = 0;

        tutorialLevelPlayed = PlayerPrefs.GetInt("Tutorial Level");

        if (tutorialLevelPlayed > notPlayedTutorial)
        {
            SceneManager.LoadScene("First Level");
        }
        else
        {

        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
