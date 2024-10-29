using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private int health = 3;
    [SerializeField] private UnityEngine.UI.Image[] healthImages;
    [SerializeField] private Sprite fullHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
  private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim =  GetComponent<Animator>();

        UpdateHealthUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            Die();

        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            anim.SetBool("enemyHitting", true);
            HealthControl();
        }
        else
            anim.SetBool("enemyHitting", false);


    }
    private void HealthControl()
    {
        if (health == 0)
        {
            Die();
        }
        else
            health--;
        UpdateHealthUI();
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        RestartLevel();
    }
    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < health)
            {
                healthImages[i].sprite = fullHealthSprite;
            }
            else
            {
                healthImages[i].sprite = emptyHealthSprite; 
            }
        }
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
