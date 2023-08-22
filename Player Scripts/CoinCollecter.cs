using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CoinCollecter : MonoBehaviour
{
    #region Parameter

    #region Const

    private const string TextTagText = "Text";

    private const string CoinTagText = "Coin";

    #endregion

    #region CoinManager 

    private CoinManager coinManager = null;

    #endregion

    #endregion


    #region Start

    private void Start()
    {
        coinManager = GameObject.FindGameObjectWithTag(TextTagText).GetComponent<CoinManager>();
    }

    #endregion

    #region OnTriggerEnter2D

    /// <summary>
    /// If the player collides with the coin the AddCoin Class from the coinManager will be started and then the coin will
    /// be destroyed
    /// In the CoinManager the number will be changed to the new correctly number of coins
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == CoinTagText)
        {
            coinManager.AddCoin();

            Destroy(collision.gameObject);
        }
    }

    #endregion
}
