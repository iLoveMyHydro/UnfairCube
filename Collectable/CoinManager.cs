using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    //Coin System wurde zu teilen aus der 3D Steuerung sowie aus folgendem Video erstellt:
    //https://youtu.be/Dwkzdimdk9I

    [SerializeField] private int coin = 0;
    [SerializeField] private Text coinText;


    // Update is called once per frame
    void Update()
    {
        coinText.text = coin.ToString();
    }

    public void AddCoin()
    {
        coin++;
    }
}
