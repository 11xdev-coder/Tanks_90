using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUp : PowerUps
{
    public Health _health;
    protected override void Start()
    {
        base.Start();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Health.currentHealth++;
        print(Health.currentHealth);
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        GPM.UpdatePlayerLives();
        Destroy(this.gameObject);
    }
}
