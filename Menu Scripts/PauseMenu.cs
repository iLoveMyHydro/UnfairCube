using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(InputActionProperty))]
public class PauseMenu : MonoBehaviour
{
    //Das Pause Menu wurde mit folgenden Quellen teilweise erstellt:
    //https://youtu.be/G1AQxNAQV8g
    //Marcus Hilfe
    //Stephan Hilfe
    //https://youtu.be/JivuXdrIHK0

    #region Parameter

    #region Const

    private const string MainMenuSceneText = "Main Menu";

    #endregion

    #region Not Sortable

    public bool gameIsPause { get; set; } = false;

    [SerializeField] private GameObject gameMenuUI;

    [SerializeField] private InputActionProperty actionProperty = default;

    #endregion

    #endregion


    #region Awake

    private void Awake()
    {
        if (gameMenuUI == null) gameMenuUI = GetComponent<GameObject>();

        if (actionProperty == null) actionProperty = GetComponent<InputActionProperty>();
    }

    #endregion

    #region Start

    /// <summary>
    /// If the player pressed the Escape Button the OptionsMenu will be started
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        actionProperty.action.performed += OptionsMenu;
    }

    #endregion

    #region OptionsMenu

    /// <summary>
    /// If the Escape Button is presse
    /// The Game is either in Pause Mode or in Resume
    /// </summary>
    /// <param name="context"></param>
    public void OptionsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    #endregion

    #region Resume

    /// <summary>
    /// The UI wont be displayed 
    /// gameIsPause is false
    /// player will be walkable again
    /// </summary>
    public void Resume()
    {
        gameMenuUI.SetActive(false);
        GameManager.Instance.IsPaused = false;
        gameIsPause = false;
    }

    #endregion

    #region Pause

    /// <summary>
    /// The UI will be active
    /// gameIsPause is true
    /// Player cant be moved
    /// </summary>
    private void Pause()
    {
        gameMenuUI.SetActive(true);
        GameManager.Instance.IsPaused = true;
        gameIsPause = true;
    }

    #endregion

    #region Load Menu

    /// <summary>
    /// If the player presses the Main Menu Button the Main Menu Scene will be loaded
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(MainMenuSceneText);
    }

    #endregion

    #region QuitGame

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
