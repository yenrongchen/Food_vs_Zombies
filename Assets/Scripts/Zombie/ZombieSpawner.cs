using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;

    private GameObject zombieObj;

    [SerializeField]
    private float waveStrengthDiff = 1f;  // change the strength difference between waves after wave 7

    [SerializeField]
    private float wave1HpMultiplier = 1f;
    
    [SerializeField]
    private float wave1SpeedMultiplier = 1f;
    
    [SerializeField]
    private float wave1AttackMultiplier = 1f;
    
    [SerializeField]
    private float wave2HpMultiplier = 1.25f;
    
    [SerializeField]
    private float wave2SpeedMultiplier = 1.25f;
    
    [SerializeField]
    private float wave2AttackMultiplier = 1.25f;
    
    [SerializeField]
    private float wave3HpMultiplier = 1.5f;
    
    [SerializeField]
    private float wave3SpeedMultiplier = 1.375f;
    
    [SerializeField]
    private float wave3AttackMultiplier = 1.5f;

    [SerializeField]
    private float waveTime = 24f;

    [SerializeField]
    private float minInterval = 5f;

    [SerializeField]
    private float maxInterval = 8f;

    private Vector3 spawnPoint1 = new Vector3(9.5f, -9.2f, 0f);
    private Vector3 spawnPoint2 = new Vector3(9.5f, -10.7f, 0f);
    private Vector3 spawnPoint3 = new Vector3(9.5f, -12.2f, 0f);
    private Vector3 spawnPoint4 = new Vector3(9.5f, -13.6f, 0f);
    private Vector3 spawnPoint5 = new Vector3(9.5f, -15.1f, 0f);

    // testing
    private void Start()
    {

    }

    public void spawnWave(int waveNum)
    {
        if (waveNum == 1)
        {
            StartCoroutine(Spawn(spawnPoint3, waveNum));
        }
        else if (waveNum == 2)
        {
            StartCoroutine(Spawn(spawnPoint2, waveNum));
            StartCoroutine(Spawn(spawnPoint3, waveNum));
            StartCoroutine(Spawn(spawnPoint4, waveNum));
        }
        else
        {
            StartCoroutine(Spawn(spawnPoint1, waveNum));
            StartCoroutine(Spawn(spawnPoint2, waveNum));
            StartCoroutine(Spawn(spawnPoint3, waveNum));
            StartCoroutine(Spawn(spawnPoint4, waveNum));
            StartCoroutine(Spawn(spawnPoint5, waveNum));
        }
    }

    IEnumerator Spawn(Vector3 spawnPoint, int waveNum)
    {
        if (waveNum == 1)
        {
            yield return spawnOneLine(spawnPoint, wave1HpMultiplier, wave1SpeedMultiplier, wave1AttackMultiplier, waveNum);
        }
        else if (waveNum == 2)
        {
            yield return spawnOneLine(spawnPoint, wave2HpMultiplier, wave2SpeedMultiplier, wave2AttackMultiplier, waveNum);
        }
        else if (waveNum == 3)
        {
            yield return spawnOneLine(spawnPoint, wave3HpMultiplier, wave3SpeedMultiplier, wave3AttackMultiplier, waveNum);
        }
        else
        {
            switch (waveNum % 3)
            {
                case 1:
                    float hpMul = (waveNum - 4) / 3 * waveStrengthDiff * (waveNum / 2 - 1) + 2f;
                    yield return spawnOneLine(spawnPoint, hpMul, hpMul - waveStrengthDiff, hpMul - waveStrengthDiff, waveNum);
                    break;

                case 2:
                    float speedMul = (waveNum - 5) / 3 * waveStrengthDiff * 0.8f + 2f;
                    yield return spawnOneLine(spawnPoint, speedMul - waveStrengthDiff, speedMul, speedMul - waveStrengthDiff, waveNum);
                    break;

                case 0:
                    float atkMul = (waveNum - 6) / 3 * waveStrengthDiff + 2f;
                    yield return spawnOneLine(spawnPoint, atkMul - waveStrengthDiff, atkMul - waveStrengthDiff, atkMul, waveNum);
                    break;
            }
        }
    }

    IEnumerator spawnOneLine(Vector3 spawnPoint, float hpMul, float spdMul, float atkMul, int waveNum)
    {
        float total = 0f;
        float interval = 0f;

        while (total <= waveTime)
        {
            if (waveNum < 7)
            {
                interval = Random.Range(minInterval, maxInterval);
            }
            else
            {
                interval = Random.Range(minInterval / 2, maxInterval / 2);
            }
            
            if (total + interval > waveTime)
            {
                break;
            }

            yield return new WaitForSeconds(interval);
            GameObject zombieInstance = Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);
            zombieInstance.GetComponent<ZombieController>().setMultiplier(hpMul, spdMul, atkMul);

            total += interval;
        }
    }
}
