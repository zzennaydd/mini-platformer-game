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

    [SerializeField] private SpriteRenderer emptyKeyRenderer;
    [SerializeField] private Sprite fullKeySprite;
    private bool keyCollected = false;
    private bool keyCollectable = false;

    [SerializeField] private AudioSource winSoundEffect;
    [SerializeField] private AudioSource collectSoundEffect;
    private void Start()
    {
        emptyKeyRenderer = GameObject.Find("key").GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        if(keyCollected == true)
        {
            winSoundEffect.Play();
            goldCoinsText.text = "You completed this level !";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldCoin"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            goldCoins++;
            goldCoinsText.text = "Gold Coins Collected: " + goldCoins;

            if (goldCoins >= 17)
            {
                emptyKeyRenderer.sprite = fullKeySprite;
                keyCollectable = true;

            }
        }    
            
        if (collision.gameObject.CompareTag("key") && keyCollectable == true)
        {
            Destroy(collision.gameObject);
            keyCollected = true;
            

        }
    }
    
    
}
