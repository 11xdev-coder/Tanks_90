using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int actualHealth;
    int currentHealth;
    Animator anime;
    Rigidbody2D rb2d;
    void Start()
    {
        SetHealth();
        anime = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            //rb2d.velocity = Vector2.zero;
            //anime.SetTrigger("killed");
            Death();
        }
    }
    public void SetHealth()
    {
        currentHealth = actualHealth;
    }
    public void SetInvincible()
    {
        currentHealth = 1000;
    }
    void Death()
    {
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>(); //newly added
        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer(); //Spawn Player
        }
        else
        {
            if (gameObject.CompareTag("Small")) MasterTracker.smallTankDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTankDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTankDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTankDestroyed++;
        }
        Destroy(gameObject);
    }
}

