using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
public class TutorialManager : MonoBehaviour
{
    #region Parameters

    #region Const

    private const string RespawnTagText = "Respawn";

    private const string TutorialLevelSceneText = "Tutorial Level";

    private const string InfoDoubleJumpTagText = "Show Text Double Jump";

    private const string FirstJumpPrefText = "First Jump";

    private const string ShowTextTagText = "ShowText";

    private const string InvisibleWallTagText = "Invisible Wall";

    private const string FallingGroundTagText = "Falling Ground";

    private const string NoInfoDoubleJumpTagText = "Dont Show Text Double Jump";

    private const string ShowTextWallJumpTagText = "Show Text Wall Jump";

    private const string WallJumpPrefText = "Wall Jump";

    private const string NoInfoWallJumpTagText = "Dont Show Text Wall Jump";

    private const string AimTagText = "Aim";

    private const string LevelHubSceneText = "LevelHub";

    private const string EnemyTagText = "Enemy";

    private const string UiGameObjectsHeaderText = "UI GameObjects";

    private const string TrickingStuffHeaderText = "Tricking Stuff";

    private const string MovementSpeedHeaderText = "Movement Speed";

    private const string JumpHeaderText = "Jump";

    private const string ResetContextMenuText = "Reset";

    #endregion

    #region UI GameObjects

    [Header(UiGameObjectsHeaderText)]
    [SerializeField] private GameObject jumpTutorialUI;
    [SerializeField] private GameObject doubleJumpUI;
    [SerializeField] private GameObject wallJumpUI;

    #endregion

    #region Tricking Stuff

    [Header(TrickingStuffHeaderText)]
    [SerializeField] private GameObject invisibleWall;
    [SerializeField] private GameObject fallingGround;
    [SerializeField] private GameObject Wall;

    #endregion

    #region Movement Speed

    [Header(MovementSpeedHeaderText)]
    [SerializeField] private Move move = null;
    [SerializeField] private float moveSpeedZero = 0f;
    [SerializeField] private float maxMoveSpeed = 4f;

    #endregion

    #region Jump

    [Header(JumpHeaderText)]
    [SerializeField] private Jump jump = null;

    #endregion

    #region Reset Editor

    [ContextMenu(ResetContextMenuText)]
    public void Reset()
    {
        var didntPlayJumpTutorial = 0;

        PlayerPrefs.SetInt("First Jump", didntPlayJumpTutorial);

        PlayerPrefs.SetInt("Wall Jump", didntPlayJumpTutorial);
    }

    #endregion

    #region Not Sortable

    private int wallJumpWrong = 0;

    #endregion

    #endregion


    #region Awake

    private void Awake()
    {
        if (move == null)
        {
            move = GetComponent<Move>();
        }
    }

    #endregion

    #region OnTriggerEnter

    public void OnTriggerEnter2D(Collider2D collision)
    {
        #region Fields

        var didntPlayJumpTutorial = 0;

        var playedJumpTutorial = 1;

        #endregion

        #region Respawn

        //If the player collides with this Trigger the Scene will reload
        if (collision.CompareTag(RespawnTagText))
        {
            SceneManager.LoadScene(TutorialLevelSceneText);
        }

        #endregion

        #region Double Jump

        //If the player collides with this Trigger and the player got the jump info and fell into ground
        //He then gets the info about the double jump
        if (collision.CompareTag(InfoDoubleJumpTagText))
        {
            if (PlayerPrefs.GetInt(FirstJumpPrefText) == playedJumpTutorial)
            {
                doubleJumpUI.SetActive(true);
            }
        }

        #endregion

        #region Show Jump Info

        //If the player collides with this Trigger and the player didnt play the tutorial once
        //Then the speed will be set to 0 - the player has to jump off the trigger and re enter the trigger
        //When the speed is set to 0 - there will be a UI that shows how to jump and that the player can jump higher if he
        //Presses the spacebar longer
        //Then the speed will be set to normal again and the player can play again

        if (collision.CompareTag(ShowTextTagText))
        {
            if (PlayerPrefs.GetInt(FirstJumpPrefText) == didntPlayJumpTutorial)
            {
                jumpTutorialUI.SetActive(true);
                move.MoveSpeed = moveSpeedZero;
                fallingGround.SetActive(true);
                invisibleWall.SetActive(true);
                jump.CanWallJump = false;
                jump.CanWallSlide = false;
                PlayerPrefs.SetInt(FirstJumpPrefText, playedJumpTutorial);

            }
            else
            {
                jumpTutorialUI.SetActive(false);
                move.MoveSpeed = maxMoveSpeed;
            }
        }

        #endregion

        #region Invisible Wall

        //If the player played the tutorial once the wall will be not active and nothing happens
        //If the player didnt play the tutorial then the wall will be active and the player cant go trough that passage
        if (collision.CompareTag(InvisibleWallTagText))
        {
            if (PlayerPrefs.GetInt(FirstJumpPrefText) == playedJumpTutorial)
            {
                invisibleWall.SetActive(false);
            }
        }

        #endregion

        #region Falling Ground

        //If the player collides with this Trigger the ground after the normal jump will disappear
        if (collision.CompareTag(FallingGroundTagText))
        {
            if (PlayerPrefs.GetInt(FirstJumpPrefText) == playedJumpTutorial)
            {
                fallingGround.SetActive(false);
            }
        }

        #endregion

        #region Stop Showing Info Double Jump

        //If the player collides with this Trigger the UI for the Double Jump wont be active anymore
        //Also the player now can Wall Jump and Wall Slide
        if (collision.CompareTag(NoInfoDoubleJumpTagText))
        {
            doubleJumpUI.SetActive(false);
            Wall.SetActive(true);
            jump.CanWallJump = true;
            jump.CanWallSlide = true;
        }


        #endregion

        #region Show Double Jump Info

        //If the player collides with this Trigger the player gets the UI how to wall jump/slide
        //Also there will be an int that watches if the player got the wall jump correctly
        //if not the tutorial level will be reloaded
        if (collision.CompareTag(ShowTextWallJumpTagText))
        {
            if (PlayerPrefs.GetInt(WallJumpPrefText) == didntPlayJumpTutorial)
            {
                wallJumpUI.SetActive(true);
            }
            wallJumpWrong++;

            if (wallJumpWrong > 1)
            {
                SceneManager.LoadScene(TutorialLevelSceneText);
            }
        }

        #endregion

        #region Stop Showing Info Wall Jump

        //If the player collides with this Trigger the Wall Jump UI wont be displayed anymore
        if (collision.CompareTag(NoInfoWallJumpTagText))
        {
            wallJumpUI.SetActive(false);
        }

        #endregion

        #region Aim

        //If the player collides with this Trigger the Tutorial Level is finished and the player will be back in the
        //levelhub
        if (collision.CompareTag(AimTagText))
        {
            PlayerPrefs.SetInt(TutorialLevelSceneText, 1);

            SceneManager.LoadScene(LevelHubSceneText);
        }

        #endregion

        #region Enemy

        //If the player collides with this Trigger the level will be reloaded
        if (collision.CompareTag(EnemyTagText))
        {
            SceneManager.LoadScene(TutorialLevelSceneText);
        }

        #endregion
    }

    #endregion
}
