using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    Tilemap waterTilemap, steelTilemap;
    [SerializeField]
    Transform tankReservePanel;
    [SerializeField]
    Text playerLivesText, stageNumber;
    GameObject tankImage;
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField]
    Text stageNumberText, gameOverText;
    [SerializeField]
    GameObject[] bonusCrates;
    GameObject[] spawnPoints, spawnPlayerPoints;
    bool stageStart = false;
    bool tankReverseEmpty = false;
    public Health _health;
    // Use this for initialization
    void Start()
    {
        steelTilemap = GameObject.Find("Steel").GetComponent<Tilemap>();
        waterTilemap = GameObject.Find("Water").GetComponent<Tilemap>();
        stageStart = true;
        StartCoroutine(StartStage());
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        UpdateTankReserve();
        UpdatePlayerLives();
        UpdateStageNumber();
    }

    private void Update()
    {
        if(tankReverseEmpty && GameObject.FindWithTag("Small") == null && GameObject.FindWithTag("Fast") == null && GameObject.FindWithTag("Big") == null && GameObject.FindWithTag("Armored") == null)
        {
            MasterTracker.stageCleared = true;
            LevelCompleted();
        }
    }

    void UpdateTankReserve()
    {
        int j;
        int numberOfTanks = LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks;
        for(j = 0; j < numberOfTanks; j++)
        {
            tankImage = tankReservePanel.transform.GetChild(j).gameObject;
            tankImage.SetActive(true);
        }
    }

    bool InvalidBonusCratePosition(Vector3 cratePosition)
    {
        return waterTilemap.GetTile(waterTilemap.WorldToCell(cratePosition)) != null || steelTilemap.GetTile(steelTilemap.WorldToCell(cratePosition)) != null;
    }

    public void GenerateBonusCrate()
    {
        GameObject bonusCrate = bonusCrates[Random.Range(0, bonusCrates.Length)];
        Vector3 cratePosition = new Vector3(Random.Range(-12, 12), Random.Range(-12, 13), 0);
        if (InvalidBonusCratePosition(cratePosition))
        {
            do
            {
                cratePosition = new Vector3(Random.Range(-12, 12), Random.Range(-12, 13), 0);
                if (!InvalidBonusCratePosition(cratePosition))
                {
                    Instantiate(bonusCrate, cratePosition, Quaternion.identity);
                }
            } while (InvalidBonusCratePosition(cratePosition));
        }
        else
        {
            Instantiate(bonusCrate, cratePosition, Quaternion.identity);
        }
    }

    public void RemoveTankReserve()
    {
        int numberOfTanks = LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks;
        tankImage = tankReservePanel.transform.GetChild(numberOfTanks).gameObject;
        tankImage.SetActive(false);
    }

    public void UpdatePlayerLives()
    {
        playerLivesText.text = Health.currentHealth.ToString();
    }

    public void UpdateStageNumber()
    {
        stageNumber.text = MasterTracker.stageNumber.ToString();
    }

    private void LevelCompleted()
    {
        tankReverseEmpty = false;
        SceneManager.LoadScene("Score");
    }

    public void SpawnEnemy()
    {
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            CancelInvoke();
            tankReverseEmpty = true;
        }
    }

    public void SpawnPlayer()
    {
        if (MasterTracker.playerLives > 0)
        {
            if (!stageStart)
            {
                MasterTracker.playerLives--;
            }
            stageStart = false;
            Animator anime = spawnPlayerPoints[0].GetComponent<Animator>();
            //anime.SetTrigger("Spawning");
        }
        else
        {
            StartCoroutine(GameOver());
       }
    }

    IEnumerator StartStage()
    {
        //StartCoroutine(GameOver());
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
        SpawnPlayer();
    }

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            stageNumberText.text = "STAGE " + MasterTracker.stageNumber.ToString();
            blackCurtain.rectTransform.localScale = new Vector3(1, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }
    IEnumerator RevealTopStage()
    {
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < 1250)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > -400)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 120f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterTracker.stageCleared = false;
        LevelCompleted();
    }
}