using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    #region Parameter

    #region Const

    private const string MoveHeaderText = "Move";

    #endregion

    #region Move

    [Header(MoveHeaderText)]
    [SerializeField] private new Rigidbody2D rigidbody;
    [field: SerializeField] public float MoveSpeed { get; set; } = 4.0f;

    #endregion

    #endregion


    #region Awake

    void Awake()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region FixedUpdate

    // Update is called once per frame
    //If the player isnt paused the player will move constantly with the MoveSpeed
    void FixedUpdate()
    {
        if (GameManager.Instance?.IsPaused == false)
        {
            rigidbody.velocity = new Vector2(MoveSpeed, rigidbody.velocity.y);
            rigidbody.isKinematic = false;
        }
        else
        {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
        }
    }

    #endregion
}
