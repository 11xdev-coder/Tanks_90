using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{
    public int speed;
    public Transform shootPos;
    public GameObject chuckPrefab;
    public GameObject losePanel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentChuck = Instantiate(chuckPrefab, shootPos.transform.position, transform.rotation) as GameObject;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + Vector3.left / speed;
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Vector3.right / speed;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + Vector3.up / speed;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + Vector3.down / speed;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("chuck"))
        {
            StartCoroutine(Lose());
        }
    }

    IEnumerator Lose()
    {
        losePanel.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainMenu");
        losePanel.SetActive(false);
    }
}
