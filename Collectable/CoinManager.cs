using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    //Coin System wurde zu teilen aus der 3D Steuerung sowie aus folgendem Video erstellt:
    //https://youtu.be/Dwkzdimdk9I

    #region Parameters

    #region Const

    private const string CoinStuffHeaderText = "Coin Stuff";

    #endregion

    #region Coin Stuff

    [Header(CoinStuffHeaderText)]
    [SerializeField] private int coin = 0;
    [SerializeField] private Text coinText;

    #endregion

    #endregion

    #region Update

    // Update is called once per frame
    void Update()
    {
        coinText.text = coin.ToString();
    }

    #endregion

    #region AddCoin
    
    /// <summary>
    /// If the player collects one Coin coin will be ++
    /// </summary>
    public void AddCoin()
    {
        coin++;
    }

    #endregion
}
