using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;

    private GameObject zombieObj;

    private float waveStrengthDiff = 0.5f;  // change the strength difference between waves after wave 7

    private Vector3 spawnPoint1 = new Vector3(9.5f, 2.8f, 0f);
    private Vector3 spawnPoint2 = new Vector3(9.5f, 1.3f, 0f);
    private Vector3 spawnPoint3 = new Vector3(9.5f, -0.2f, 0f);
    private Vector3 spawnPoint4 = new Vector3(9.5f, -1.6f, 0f);
    private Vector3 spawnPoint5 = new Vector3(9.5f, -3.1f, 0f);

    // testing
    private void Start()
    {
        //StartCoroutine(Spawn(spawnPoint1, 1));
        //StartCoroutine(Spawn(spawnPoint2, 1));
        //StartCoroutine(Spawn(spawnPoint3, 1));
        //StartCoroutine(Spawn(spawnPoint4, 1));
        //StartCoroutine(Spawn(spawnPoint5, 1));
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
            yield return spawnOneLine(spawnPoint, 1f, 1f, 1f);
        }
        else if (waveNum == 2)
        {
            yield return spawnOneLine(spawnPoint, 1.5f, 1f, 1.25f);
        }
        else if (waveNum == 3)
        {
            yield return spawnOneLine(spawnPoint, 1.5f, 1.5f, 1.5f);
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
        float interval = Random.Range(3f, 12f);
        yield return new WaitForSeconds(interval);
        GameObject zombieInstance = Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);
        zombieInstance.GetComponent<ZombieController>().setMultiplier(hpMul, spdMul, atkMul);

        for (int i = 0; i < 3; i++)
        {
            interval = Random.Range(10f, 20f);
            yield return new WaitForSeconds(interval);
            zombieInstance = Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);
            zombieInstance.GetComponent<ZombieController>().setMultiplier(hpMul, spdMul, atkMul);
        }
    }
}
