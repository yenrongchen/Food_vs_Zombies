using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;

    public GameObject sp1;
    public GameObject sp2;
    public GameObject sp3;
    public GameObject sp4;
    public GameObject sp5;


    public void spawnWave(int waveNum)
    {
        if (waveNum == 1)
        {
            StartCoroutine(Spawn(sp3));
        }
        else if (waveNum == 2)
        {
            StartCoroutine(Spawn(sp2));
            StartCoroutine(Spawn(sp3));
            StartCoroutine(Spawn(sp4));
        }
        else
        {
            StartCoroutine(Spawn(sp1));
            StartCoroutine(Spawn(sp2));
            StartCoroutine(Spawn(sp3));
            StartCoroutine(Spawn(sp4));
            StartCoroutine(Spawn(sp5));
        }
    }

    IEnumerator Spawn(GameObject spawner)
    {
        for (int i = 0; i < 3; i++)
        {
            float interval = Random.Range(5f, 10f);
            yield return new WaitForSeconds(interval);
            Instantiate(zombie, spawner.transform.position, spawner.transform.rotation);
        }
    }
}
