using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TallyScore : MonoBehaviour
{
    [SerializeField]
    Text hiScoreText, stageText, playerScoreText, smallTankScoreText, fastTankScoreText, bigTankScoreText, armoredTankScoreText, smallTanksDestroyed, fastTanksDestroyed, bigTanksDestroyed, armoredTanksDestroyed, totalTanksDestroyed;
    int smallTankScore, fastTankScore, bigTankScore, armoredTankScore;
    MasterTracker masterTracker;
    int smallTankPointsWorth, fastTankPointsWorth, bigTankPointsWorth, armoredTankPointsWorth;
    // Use this for initialization
    void Start()
    {
        masterTracker = GameObject.Find("MasterTracker").GetComponent<MasterTracker>();
        smallTankPointsWorth = masterTracker.smallTankPointsWorth;
        fastTankPointsWorth = masterTracker.fastTankPointsWorth;
        bigTankPointsWorth = masterTracker.bigTankPointsWorth;
        armoredTankPointsWorth = masterTracker.armoredTankPointsWorth;
        stageText.text = "STAGE " + MasterTracker.stageNumber;
        playerScoreText.text = MasterTracker.playerScore.ToString();
        StartCoroutine(UpdateTankPoints());
    }
    IEnumerator UpdateTankPoints()
    {
        for (int i = 0; i <= MasterTracker.smallTankDestroyed; i++)
        {
            smallTankScore = smallTankPointsWorth * i;
            smallTankScoreText.text = smallTankScore.ToString();
            smallTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i <= MasterTracker.fastTankDestroyed; i++)
        {
            fastTankScore = fastTankPointsWorth * i;
            fastTankScoreText.text = fastTankScore.ToString();
            fastTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i <= MasterTracker.bigTankDestroyed; i++)
        {
            bigTankScore = bigTankPointsWorth * i;
            bigTankScoreText.text = bigTankScore.ToString();
            bigTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i <= MasterTracker.armoredTankDestroyed; i++)
        {
            armoredTankScore = armoredTankPointsWorth * i;
            armoredTankScoreText.text = armoredTankScore.ToString();
            armoredTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        totalTanksDestroyed.text = (MasterTracker.smallTankDestroyed + MasterTracker.fastTankDestroyed + MasterTracker.bigTankDestroyed + MasterTracker.armoredTankDestroyed).ToString();
        MasterTracker.playerScore += (smallTankScore + fastTankScore + bigTankScore + armoredTankScore);
        yield return new WaitForSeconds(5f);
        if (MasterTracker.stageCleared)
        {
            ClearStatistics();
            SceneManager.LoadScene("Stage" + (MasterTracker.stageNumber + 1));
        }
        else
        {
            ClearStatistics();
            //Load GameOver Scene
        }
    }
    void ClearStatistics()
    {
        MasterTracker.smallTankDestroyed = 0;
        MasterTracker.fastTankDestroyed = 0;
        MasterTracker.bigTankDestroyed = 0;
        MasterTracker.armoredTankDestroyed = 0;
        MasterTracker.stageCleared = false;
    }
}
