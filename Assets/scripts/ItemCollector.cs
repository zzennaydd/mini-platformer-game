using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    
    private int goldCoins = 0;
    [SerializeField] private TextMeshProUGUI goldCoinsText;

    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private Finish finish;
    private bool coinsCollected = false;
    public bool keyCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldCoin"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            goldCoins++;
            if (goldCoinsText != null)
            {
                goldCoinsText.text = "Gold Coins Collected: " + goldCoins;
            }
            if(goldCoins >= 17)
            {
                coinsCollected = true;
                goldCoinsText.text = "You completed this level!";
            }
           if(coinsCollected)
            {
                finish.FinishLevel();
            }
        }
        if(collision.gameObject.CompareTag("key"))
        {
            Destroy(collision.gameObject);
            keyCollected = true;
        }
    }
 
}
