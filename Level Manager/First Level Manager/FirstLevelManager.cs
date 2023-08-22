using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelManager : MonoBehaviour
{
    #region Parameters

    #region Const

    private const string TrickingStuffHeaderText = "Tricking Stuff";

    private const string MovementHeaderText = "Movement";

    private const string EnemyTagText = "Enemy";

    private const string FirstLevelSceneText = "First Level";

    private const string TutorialLevelSceneText = "Tutorial Level";

    private const string RespawnTagText = "Respawn";

    private const string ResetPlayerWallOneTagText = "Reset Player Wall 1";

    private const string ResetPlayerWallTwoTagText = "Reset Player Wall 2";

    private const string SpeedIncreaseTagText = "Speed Increase";

    private const string SpeedDecreaseTagText = "Speed Decrease";

    private const string FinalCheckpointTagText = "Final Checkpoint";

    private const string OpenSecretDoorTagText = "Open Secret Door";

    #endregion

    #region Tricking Stuff

    [Header(TrickingStuffHeaderText)]
    [SerializeField] private int checkPointChecker = 0;

    [SerializeField] private GameObject secretWall;
    [SerializeField] private GameObject secretWallVisible;

    [SerializeField] private GameObject visibleWallDown;
    [SerializeField] private GameObject visibleWallUp;

    #endregion

    #region Movement

    [Header(MovementHeaderText)]
    [SerializeField] private Move move = null;
    [SerializeField] private float maxMoveSpeed = 50f;
    [SerializeField] private float normalMoveSpeed = 4f;

    #endregion

    #region Not Sortable

    private int wallJumpWrong = 0;

    #endregion

    #endregion


    #region OnTriggerEnter

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Fields

        var spawn = 0;

        var firstCheckpoint = 1;

        var secondCheckpoint = 2;

        var xAxisFirstCheckpoint = -125;

        var yAxisFirstCheckpoint = 10;

        var xAxisSecondCheckpoint = 125;

        var yAxisSecondCheckpoint = 30;

        var wrongDirectionWallOne = 1;

        var wrongDirectionWallTwo = 3;

        #endregion

        #region Enemy
        //If the player collides with this Trigger 
        //The Level will be reloaded if the player didnt reached the first checkpoint 
        //If the player got to the first checkpoint the player will be set to the checkpoint and then the level goes on
        //If the player got to the second checkpoint the player will be set to the checkpoint and then the level goes on
        if (collision.CompareTag(EnemyTagText))
        {
            if (checkPointChecker == spawn)
            {
                SceneManager.LoadScene(FirstLevelSceneText);
            }
            else if (checkPointChecker == firstCheckpoint)
            {
                transform.position = new Vector2(xAxisFirstCheckpoint, yAxisFirstCheckpoint);
            }
            else if (checkPointChecker == secondCheckpoint)
            {
                transform.position = new Vector2(xAxisSecondCheckpoint, yAxisSecondCheckpoint);
            }
        }

        #endregion

        #region Respawn

        //If the player collides with this Trigger 
        //The Level will be reloaded if the player didnt reached the first checkpoint 
        //If the player got to the first checkpoint the player will be set to the checkpoint and then the level goes on
        //If the player got to the second checkpoint the player will be set to the checkpoint and then the level goes on
        if (collision.CompareTag(RespawnTagText))
        {
            if (checkPointChecker == spawn)
            {
                SceneManager.LoadScene(FirstLevelSceneText);
            }
            else if (checkPointChecker == firstCheckpoint)
            {
                transform.position = new Vector2(xAxisFirstCheckpoint, yAxisFirstCheckpoint);
            }
            else if (checkPointChecker == secondCheckpoint)
            {
                transform.position = new Vector2(xAxisSecondCheckpoint, yAxisSecondCheckpoint);
            }
        }

        #endregion

        #region Reset Player Wall 
        //If the player dont get the Wall Jump correctly the scene will be reloaded
        #region Reset Player Wall 1

        if (collision.CompareTag(ResetPlayerWallOneTagText))
        {
            wallJumpWrong++;

            if (wallJumpWrong > wrongDirectionWallOne)
            {
                SceneManager.LoadScene(FirstLevelSceneText);
            }
        }

        #endregion

        #region Reset Player Wall 2

        if (collision.CompareTag(ResetPlayerWallTwoTagText))
        {
            wallJumpWrong++;

            if (wallJumpWrong > wrongDirectionWallTwo)
            {
                SceneManager.LoadScene(FirstLevelSceneText);
            }
        }

        #endregion

        #endregion

        #region Speed Change

        //If the player get the short cut/easter egg
        //His speed will be first increased to 50
        //and then the speed will be decreased at the end to normal again
        #region Speed Increase

        if (collision.CompareTag(SpeedIncreaseTagText))
        {
            move.MoveSpeed = maxMoveSpeed;
        }

        #endregion

        #region Speed Decrease

        if (collision.CompareTag(SpeedDecreaseTagText))
        {
            move.MoveSpeed = normalMoveSpeed;
        }

        #endregion

        #endregion

        #region CheckPoints
        //If the player goes through the checkpoints then he will be teleported to the checkpoint 
        #region First Checkpoint

        if (collision.CompareTag(FinalCheckpointTagText))
        {
            if (checkPointChecker == spawn)
            {
                checkPointChecker++;
            }

            if (checkPointChecker == firstCheckpoint)
            {
                SceneManager.LoadScene("LevelHub");
            }
        }

        #endregion



        #endregion

        #region Open Secret Door 

        //If the player collides with this trigger 
        //The wall will be deactivated and the player can go through the wall now
        if (collision.CompareTag(OpenSecretDoorTagText))
        {
            secretWall.SetActive(false);
            secretWallVisible.SetActive(true);

            visibleWallDown.SetActive(true);
            visibleWallUp.SetActive(true);
        }

        #endregion
    }

    #endregion
}
