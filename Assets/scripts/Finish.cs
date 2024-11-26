using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldCoinsText;
    private Animator anim;
    private bool levelFinished = false;

    [SerializeField] private AudioSource winSoundEffect;

    void Start()
    {
        anim = GetComponent<Animator>();

    }
    public void FinishLevel()
    {
        levelFinished = true;
        winSoundEffect.Play();
        anim.SetBool("coinsCollected", true);
        Debug.Log("flag is waving.");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (levelFinished)
            {
                Invoke("StartNextLevel", 2f);
                Debug.Log("starting next level now...");
            }
        }
    }
    private void StartNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

}
  
