using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Parameters

    #region Not Sortable

    private float amplitude = 0.25f;
    private float frequency = 1.0f;

    private Vector3 startPosition;

    #endregion

    #endregion


    #region Start

    private void Start()
    {
        startPosition = transform.position;
    }

    #endregion

    #region FixedUpdate

    //Chat GPT hat mir diese Lösung gegeben -> Der Coin wird so auf der Stelle hoch und runter bewegt
    //Mit hilfe von der amplitude und der frequency
    private void FixedUpdate()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        Vector3 newPosition = new Vector3(startPosition.x, newY, startPosition.z);

        transform.position = newPosition;
    }
    #endregion
}
