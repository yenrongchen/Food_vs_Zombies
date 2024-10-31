using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private int currentWaveNum = 0;

    [SerializeField]
    private float INTERVAL_WAVE = 30f;

    [SerializeField]
    private float initInterval = 15f;

    [SerializeField]
    private float intervalWave;

    [SerializeField]
    private float animationDuration = 1f;

    [SerializeField]
    private Image oneRow;

    [SerializeField]
    private Image threeRow;

    private GameObject ZombieSpawner;
    private GameObject GridManager;

    private bool isAnimating;
    private float animTimer;

    // Start is called before the first frame update
    void Start()
    {
        ZombieSpawner = GameObject.Find("ZombieSpawner");
        GridManager = GameObject.Find("GridManager");
        intervalWave = 0;

        animTimer = 0f;
        isAnimating = false;

        if (ZombieSpawner == null || GridManager == null)
        {
            Debug.Log("ERROR in WVMG");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            if (currentWaveNum == 2)
            {
                fillRoad(oneRow);
            }
            else if (currentWaveNum == 3)
            {
                fillRoad(threeRow);
            }
            return;
        }

        if (initInterval > 0)            // set a blank reation time for player
        {
            initInterval -= Time.deltaTime;
            return;
        }

        if (intervalWave <= 0)
        {
            currentWaveNum++;

            if (currentWaveNum == 2 || currentWaveNum == 3)
            {
                animTimer = 0f;
                isAnimating = true;
            }

            ZombieSpawner.GetComponent<ZombieSpawner>().spawnWave(currentWaveNum);
            GridManager.GetComponent<GridManager>().setWave(currentWaveNum);

            intervalWave = INTERVAL_WAVE;
        }
        else
        {
            intervalWave -= Time.deltaTime;
        }

    }


    private void fillRoad(Image image)
    {
        animTimer += Time.deltaTime;

        image.fillAmount = Mathf.Lerp(1f, 0f, animTimer / animationDuration);

        if (image.fillAmount == 0f)
        {
            image.fillAmount = 0f;
            isAnimating = false;
            
            ZombieSpawner.GetComponent<ZombieSpawner>().spawnWave(currentWaveNum);
            GridManager.GetComponent<GridManager>().setWave(currentWaveNum);
        }
    }
}
