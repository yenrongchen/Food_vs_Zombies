using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;

    private Vector3 spawnPoint1 = new Vector3(9.5f, 2.8f, 0f);
    private Vector3 spawnPoint2 = new Vector3(9.5f, 1.3f, 0f);
    private Vector3 spawnPoint3 = new Vector3(9.5f, -0.2f, 0f);
    private Vector3 spawnPoint4 = new Vector3(9.5f, -1.6f, 0f);
    private Vector3 spawnPoint5 = new Vector3(9.5f, -3.1f, 0f);

    //testing
    //private void Start()
    //{
    //    StartCoroutine(Spawn(spawnPoint1));
    //    StartCoroutine(Spawn(spawnPoint2));
    //    StartCoroutine(Spawn(spawnPoint3));
    //    StartCoroutine(Spawn(spawnPoint4));
    //    StartCoroutine(Spawn(spawnPoint5));
    //}

    public void spawnWave(int waveNum)
    {
        if (waveNum == 1)
        {
            StartCoroutine(Spawn(spawnPoint3));
        }
        else if (waveNum == 2)
        {
            StartCoroutine(Spawn(spawnPoint2));
            StartCoroutine(Spawn(spawnPoint3));
            StartCoroutine(Spawn(spawnPoint4));
        }
        else
        {
            StartCoroutine(Spawn(spawnPoint1));
            StartCoroutine(Spawn(spawnPoint2));
            StartCoroutine(Spawn(spawnPoint3));
            StartCoroutine(Spawn(spawnPoint4));
            StartCoroutine(Spawn(spawnPoint5));
        }
    }

    IEnumerator Spawn(Vector3 spawnPoint)
    {
        for (int i = 0; i < 3; i++)
        {
            float interval = Random.Range(10f, 20f);
            yield return new WaitForSeconds(interval);
            Instantiate(zombie, spawnPoint, Quaternion.identity);  // no rotation
        }
    }
}
