using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helmet : PowerUps
{
    public Health _health;
    //Text playerInvincible;
    protected override void Start()
    {
        base.Start();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("SetttingHealth");
        print(_health.currentHealth);
        Destroy(this.gameObject);
    }

    IEnumerator SetttingHealth()
    {
        _health.SetInvincible();
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        GPM.UpdatePlayerLives();
        //GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        //playerInvincible = GPM.playerInvincibleText;
        //Text playerInvincible.IsActive() = !playerInvincible.IsActive();
        yield return new WaitForSeconds(5f);
        _health.SetHealth();
        GPM.UpdatePlayerLives();
    }
}
