using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private int currentWaveNum = 0;

    [SerializeField]
    private float INTERVAL_WAVE = 15f;

    [SerializeField]
    private float initInterval = 10f;

    [SerializeField]
    private float intervalWave;

    private GameObject ZombieSpawner;
    // Start is called before the first frame update
    void Start()
    {
        ZombieSpawner = GameObject.Find("ZombieSpawner");
        intervalWave = 0;

        if (ZombieSpawner == null) 
        {
            Debug.Log("ERROR in WVMG");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (initInterval > 0)            // set a blank reation time for player
        {
            initInterval -= Time.deltaTime;

            return;
        }

        if(intervalWave <= 0)
        {
            currentWaveNum++;
            ZombieSpawner.GetComponent<ZombieSpawner>().spawnWave(currentWaveNum);

            intervalWave = INTERVAL_WAVE;
        }
        else
        {
            intervalWave -= Time.deltaTime;
        }

    }
}
