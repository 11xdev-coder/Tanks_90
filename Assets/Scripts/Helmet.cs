using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helmet : PowerUps
{
    //Text playerInvincible;
    protected override void Start()
    {
        base.Start();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("SetttingHealth");
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        print(GPM.playerTank.GetComponent<Health>().currentHealth);
        Destroy(this.gameObject);
    }

    IEnumerator SetttingHealth()
    {
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        GPM.playerTank.GetComponent<Health>().SetInvincible();
        GPM.UpdatePlayerLives();
        //GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        //playerInvincible = GPM.playerInvincibleText;
        //Text playerInvincible.IsActive() = !playerInvincible.IsActive();
        yield return new WaitForSeconds(5f);
        GPM.playerTank.GetComponent<Health>().SetHealth();
        GPM.UpdatePlayerLives();
    }
}
