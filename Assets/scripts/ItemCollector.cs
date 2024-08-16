using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int goldCoins = 0;
    [SerializeField] private TextMeshProUGUI goldCoinsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GoldCoin"))
        {
            Destroy(collision.gameObject);
            goldCoins++;
            goldCoinsText.text = "Gold Coins Collected: " + goldCoins;
        }
    }
}
