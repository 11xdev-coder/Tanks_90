using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChuckController : MonoBehaviour
{

    public int shootSpeed;
    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    void Move()
    {
        transform.Translate(Vector2.up / shootSpeed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        CancelInvoke("Move");
        Destroy(gameObject);
    }
}
