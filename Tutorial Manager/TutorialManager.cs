using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
public class TutorialManager : MonoBehaviour
{

    [Header("UI GameObjects")]
    [SerializeField] private GameObject jumpTutorialUI;
    [SerializeField] private GameObject doubleJumpUI;
    [SerializeField] private GameObject wallJumpUI;

    [Header("Tricking Stuff")]
    [SerializeField] private GameObject invisibleWall;
    [SerializeField] private GameObject fallingGround;
    [SerializeField] private GameObject Wall;

    [Header("Movement speed")]
    [SerializeField] private Move move = null;

    [ContextMenu("Reset")]
    public void Reset()
    {
        var didntPlayJumpTutorial = 0;

        PlayerPrefs.SetInt("First Jump", didntPlayJumpTutorial);
    }

    private void Awake()
    {
        if (move == null)
        {
            move = GetComponent<Move>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        var zeroSpeed = 0f;

        var maxSpeed = 4f;

        var didntPlayJumpTutorial = 0;

        var playedJumpTutorial = 1;


        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene("Tutorial Level");
        }

        if (collision.CompareTag("Show Text Double Jump"))
        {
            if (PlayerPrefs.GetInt("First Jump") == playedJumpTutorial)
            {
                doubleJumpUI.SetActive(true);
            }
        }

        if (collision.CompareTag("ShowText"))
        {
            if (PlayerPrefs.GetInt("First Jump") == didntPlayJumpTutorial)
            {
                jumpTutorialUI.SetActive(true);
                move.MoveSpeed = zeroSpeed;
                fallingGround.SetActive(true);
                invisibleWall.SetActive(true);
                PlayerPrefs.SetInt("First Jump", playedJumpTutorial);

            }
            else
            {
                jumpTutorialUI.SetActive(false);
                move.MoveSpeed = maxSpeed;
            }
        }

        if (collision.CompareTag("Invsible Wall"))
        {
            if (PlayerPrefs.GetInt("First Jump") == playedJumpTutorial)
            {
                invisibleWall.SetActive(false);
            }
        }

        if (collision.CompareTag("Falling Ground"))
        {
            if (PlayerPrefs.GetInt("First Jump") == playedJumpTutorial)
            {
                fallingGround.SetActive(false);
            }
        }



        if (collision.CompareTag("Dont Show Text Double Jump"))
        {
            doubleJumpUI.SetActive(false);
            Wall.SetActive(true);
        }

        if (collision.CompareTag("Show Text Wall Jump"))
        {
            if (PlayerPrefs.GetInt("Wall Jump") == didntPlayJumpTutorial)
            {
                wallJumpUI.SetActive(true);
                PlayerPrefs.SetInt("Wall Jump", playedJumpTutorial);
            }
            else
            {
                wallJumpUI.SetActive(false);
            }

        }
    }
}
