using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private TextMeshProUGUI showWave;

    // Start is called before the first frame update
    void Start()
    {
        ZombieSpawner = GameObject.Find("ZombieSpawner");
        GridManager = GameObject.Find("GridManager");
        intervalWave = 0;

        animTimer = 0f;
        isAnimating = false;

        showWave.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(showWave.alpha == 0)
            {
                showWave.alpha = 1;
            }
            else
            {
                showWave.alpha = 0;
            }
        }
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
            showWave.text = "Currebt Wave: " + currentWaveNum;

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
