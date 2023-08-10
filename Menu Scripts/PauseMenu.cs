using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(InputActionProperty))]
public class PauseMenu : MonoBehaviour
{
    private bool gameIsPause = false;

    [SerializeField] private GameObject gameMenuUI;
    [SerializeField] private InputActionProperty actionProperty = default;

    private void Awake()
    {
        if (gameMenuUI == null) gameMenuUI = GetComponent<GameObject>();

        if (actionProperty == null) actionProperty = GetComponent<InputActionProperty>();
    }

    // Start is called before the first frame update
    void Start()
    {
        actionProperty.action.performed += OptionsMenu;
    }

    // Update is called once per frame
    void Update()
    {
    }

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

    public void Resume()
    {
        gameMenuUI.SetActive(false);
        GameManager.Instance.IsPaused = false;
        gameIsPause = false;
    }

    private void Pause()
    {
        gameMenuUI.SetActive(true);
        GameManager.Instance.IsPaused = true;
        gameIsPause = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
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
