using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int actualHealth;
    public static int currentHealth = 3;
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
        if (currentHealth <= 1)
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
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer();
        }
        else
        {
            if (gameObject.CompareTag("Small")) MasterTracker.smallTanksDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTanksDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTanksDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTanksDestroyed++;
            if (gameObject.GetComponent<BonusTank>().IsBonusTankCheck()) GPM.GenerateBonusCrate();
        }
        Destroy(gameObject);
    }
}

