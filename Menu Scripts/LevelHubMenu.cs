using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHubMenu : MonoBehaviour
{
    //Das Level Hub wurde teilweise mit folgenden Quellen erstellt:
    //https://youtu.be/2XQsKNHk1vk

    #region Parameter

    #region Const

    private const string TutorialLevelPrefText = "Tutorial Level";

    private const string TutorialLevelSceneText = "Tutorial Level";

    private const string FirstLevelSceneText = "First Level";

    private const string MainMenuSceneText = "Main Menu";

    private const string ButtonText = "Level One Button";

    #endregion

    #region Button

    [Header(ButtonText)]
    [SerializeField] private Button levelOneButton;

    #endregion

    #region Not Sortable

    private int tutorialLevelPlayed = 0;

    #endregion

    #endregion


    #region Start
   
    /// <summary>
    /// If the player didnt play the tutorial yet the player cant play Level 1 - so the Button wont be interactable
    /// </summary>
    private void Start()
    {
        #region Fields

        var tutorialPlayed = 1;

        #endregion

        if (PlayerPrefs.GetInt(TutorialLevelPrefText) == tutorialPlayed)
        {
            levelOneButton.interactable = true;
        }
    }

    #endregion

    #region Tutorial Level

    /// <summary>
    /// If you press the Tutorial Button the Tutorial Level will be loaded
    /// </summary>
    public void TutorialLevel()
    {
        SceneManager.LoadScene(TutorialLevelSceneText);
    }

    #endregion

    #region First Level

    /// <summary>
    /// If the player played the the tutorial Level the Level 1 can be played and then will be loaded
    /// </summary>
    public void FirstLevel()
    {
        #region Fields

        var notPlayedTutorial = 0;

        #endregion

        tutorialLevelPlayed = PlayerPrefs.GetInt(TutorialLevelPrefText);

        if (tutorialLevelPlayed > notPlayedTutorial)
        {
            SceneManager.LoadScene(FirstLevelSceneText);
        }
    }

    #endregion

    #region Load Menu

    /// <summary>
    /// If the player press the Main Menu button the Main Menu will be loaded
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(MainMenuSceneText);
    }

    #endregion
}
