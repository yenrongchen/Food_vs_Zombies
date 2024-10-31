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
    private float waveStrengthDiff = 0.5f;  // change the strength difference between waves after wave 7

    [SerializeField]
    private float wave1HpMultiplier = 1f;
    
    [SerializeField]
    private float wave1SpeedMultiplier = 1f;
    
    [SerializeField]
    private float wave1AttackMultiplier = 1f;
    
    [SerializeField]
    private float wave2HpMultiplier = 1.5f;
    
    [SerializeField]
    private float wave2SpeedMultiplier = 1f;
    
    [SerializeField]
    private float wave2AttackMultiplier = 1.25f;
    
    [SerializeField]
    private float wave3HpMultiplier = 1.5f;
    
    [SerializeField]
    private float wave3SpeedMultiplier = 1.5f;
    
    [SerializeField]
    private float wave3AttackMultiplier = 1.5f;

    [SerializeField]
    private float waveTime = 15f;

    [SerializeField]
    private float minInterval = 3f;

    [SerializeField]
    private float maxInterval = 5f;

    private Vector3 spawnPoint1 = new Vector3(9.5f, -9.2f, 0f);
    private Vector3 spawnPoint2 = new Vector3(9.5f, -10.7f, 0f);
    private Vector3 spawnPoint3 = new Vector3(9.5f, -12.2f, 0f);
    private Vector3 spawnPoint4 = new Vector3(9.5f, -13.6f, 0f);
    private Vector3 spawnPoint5 = new Vector3(9.5f, -15.1f, 0f);

    // testing
    private void Start()
    {
        /*StartCoroutine(Spawn(spawnPoint1, 1));
        StartCoroutine(Spawn(spawnPoint2, 1));
        StartCoroutine(Spawn(spawnPoint3, 1));
        StartCoroutine(Spawn(spawnPoint4, 1));
        StartCoroutine(Spawn(spawnPoint5, 1));*/
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
            yield return spawnOneLine(spawnPoint, wave1HpMultiplier, wave1SpeedMultiplier, wave1AttackMultiplier);
        }
        else if (waveNum == 2)
        {
            yield return spawnOneLine(spawnPoint, wave2HpMultiplier, wave2SpeedMultiplier, wave2AttackMultiplier);
        }
        else if (waveNum == 3)
        {
            yield return spawnOneLine(spawnPoint, wave3HpMultiplier, wave3SpeedMultiplier, wave3AttackMultiplier);
        }
        else
        {
            switch (waveNum % 3)
            {
                case 1:
                    float hpMul = (waveNum - 4) / 3 * waveStrengthDiff + 2; // NEED FIX
                    yield return spawnOneLine(spawnPoint, hpMul, hpMul - waveStrengthDiff, hpMul - waveStrengthDiff);
                    break;

                case 2:
                    float speedMul = (waveNum - 5) / 3 * waveStrengthDiff + 2; // NEED FIX
                    yield return spawnOneLine(spawnPoint, speedMul - waveStrengthDiff, speedMul, speedMul - waveStrengthDiff);
                    break;

                case 0:
                    float atkMul = (waveNum - 6) / 3 * waveStrengthDiff + 2; // NEED FIX
                    yield return spawnOneLine(spawnPoint, atkMul - waveStrengthDiff, atkMul - waveStrengthDiff, atkMul);
                    break;
            }
        }
    }

    IEnumerator spawnOneLine(Vector3 spawnPoint, float hpMul, float spdMul, float atkMul)
    {
        float total = 0f;
        float interval = 0f;

        while (total <= waveTime)
        {
            interval = Random.Range(minInterval, maxInterval);

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
