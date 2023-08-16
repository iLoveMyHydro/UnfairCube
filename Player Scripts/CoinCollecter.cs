using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollecter : MonoBehaviour
{
    private CoinManager coinManager = null;

    private void Start()
    {
        coinManager = GameObject.FindGameObjectWithTag("Text").GetComponent<CoinManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinManager.AddCoin();

            Destroy(collision.gameObject);
        }
    }
}
