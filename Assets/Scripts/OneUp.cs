using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUp : PowerUps
{
    protected override void Start()
    {
        base.Start();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        GPM.playerTank.GetComponent<Health>().currentHealth++;
        print(GPM.playerTank.GetComponent<Health>().currentHealth);
        GPM.UpdatePlayerLives();
        Destroy(this.gameObject);
    }
}
