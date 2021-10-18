using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int smallTanksInThisLevel, fastTanksInThisLevel, bigTanksInThisLevel, armoredTanksInThisLevel, stageNumber;
    public static int smallTanks, fastTanks, bigTanks, armoredTanks;
    [SerializeField]
    float spawnRateInThisLevel = 5, bonusCrateRateInThisLevel = 0.2f;
    public static float spawnRate { get; private set; }
    public static float bonusCrateRate { get; private set; }
    private void Awake()
    {
        MasterTracker.stageNumber = stageNumber;
        smallTanks = smallTanksInThisLevel;
        fastTanks = fastTanksInThisLevel;
        bigTanks = bigTanksInThisLevel;
        armoredTanks = armoredTanksInThisLevel;
        spawnRate = spawnRateInThisLevel;
        bonusCrateRate = bonusCrateRateInThisLevel;
    }
}
