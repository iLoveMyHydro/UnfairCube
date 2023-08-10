using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [Header("Move")]

    [SerializeField] private new Rigidbody2D rigidbody;
    [field: SerializeField] public float MoveSpeed { get; set; } = 4.0f;

    [SerializeField] private GameManager gm = null;

    void Awake()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();

        if (gm == null)
        {
            gm = GameManager.Instance;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gm.IsPaused == false)
        {
            rigidbody.velocity = new Vector2(MoveSpeed, rigidbody.velocity.y);
        }
        else rigidbody.velocity = Vector2.zero;
    }
}
