using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTank : MonoBehaviour
{
    SpriteRenderer body;
    SpriteRenderer body2;
    bool bonusTank = false;
    public bool IsBonusTankCheck()
    {
        return bonusTank;
    }
    
    public void MakeBonusTank()
    {
        body = gameObject.transform.Find("Hull").gameObject.GetComponent<SpriteRenderer>();
        body2 = gameObject.transform.Find("Tower").gameObject.GetComponent<SpriteRenderer>();
        bonusTank = true;
        InvokeRepeating("Blink", 0, 0.3f);
    }

    private void Blink()
    {
        if (body.color == Color.white && body2.color == Color.white)
        {
            body.color = Color.Lerp(Color.white, Color.red, 0.4f);
            body2.color = Color.Lerp(Color.white, Color.red, 0.4f);
        }
        else
        {
            body.color = Color.white;
            body2.color = Color.white;
        }
    }
}
