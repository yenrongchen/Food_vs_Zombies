using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    private bool isEmpty = false;

    [SerializeField]
    private GameObject ItemRice;

    [SerializeField]
    private GameObject ItemMeat;

    [SerializeField]
    private GameObject ItemVege;

    private GameObject CurrentOffering;

    [SerializeField] 
    private GameObject Canvas;

    private GameObject FatherBar;

    [SerializeField]
    private float TIMEINTERVALRICE = 8;
    [SerializeField]
    private float TIMEINTERVALMEAT = 5;
    [SerializeField]
    private float TIMEINTERVALVEGE = 5;

    private float timeIntervalRice;
    private float timeIntervalMeat;
    private float timeIntervalVege;

    // Start is called before the first frame update
    void Start()
    {
        isEmpty = false;

        CurrentOffering = null;
        FatherBar = null;

        timeIntervalRice = TIMEINTERVALRICE;
        timeIntervalMeat = TIMEINTERVALMEAT;
        timeIntervalVege = TIMEINTERVALVEGE;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty)  //  start count down to respawn ingredient
        {
            if (this.name == "RiceBox")                             // respawn rice
            {
                if (FatherBar == null)
                {
                    FatherBar = Instantiate(Canvas, this.transform);

                    FatherBar.GetComponentInChildren<ProgressBarController>().SetMaxVal(TIMEINTERVALRICE);
                }

                timeIntervalRice -= Time.deltaTime;
                FatherBar.GetComponentInChildren<ProgressBarController>().SetProgress(timeIntervalRice);
                if (timeIntervalRice <= 0)
                {
                    timeIntervalRice = TIMEINTERVALRICE;
                    isEmpty = false;

                    Destroy(FatherBar);
                    FatherBar = null;
                }
            }
            else if (this.name == "MeatBox")                    // respawn meat
            {
                if (FatherBar == null)
                {
                    FatherBar = Instantiate(Canvas, this.transform);

                    FatherBar.GetComponentInChildren<ProgressBarController>().SetMaxVal(TIMEINTERVALMEAT);
                }

                timeIntervalMeat -= Time.deltaTime;
                FatherBar.GetComponentInChildren<ProgressBarController>().SetProgress(timeIntervalMeat);
                if (timeIntervalMeat <= 0)
                {
                    timeIntervalMeat = TIMEINTERVALMEAT;
                    isEmpty = false;

                    Destroy(FatherBar);
                    FatherBar = null;
                }
            }
            else                                              // respawn vegetable
            {
                if (FatherBar == null)
                {
                    FatherBar = Instantiate(Canvas, this.transform);

                    FatherBar.GetComponentInChildren<ProgressBarController>().SetMaxVal(TIMEINTERVALVEGE);
                }

                timeIntervalVege -= Time.deltaTime;
                FatherBar.GetComponentInChildren<ProgressBarController>().SetProgress(timeIntervalVege);
                if (timeIntervalVege <= 0)
                {
                    timeIntervalVege = TIMEINTERVALVEGE;
                    isEmpty = false;

                    Destroy(FatherBar);
                    FatherBar = null;
                }
            }
        }
    }

    public GameObject GetItem()
    {
        if (isEmpty)
        {
            return null;
        }

        isEmpty = true;

        if(this.name == "RiceBox")
        {
            CurrentOffering = Instantiate(ItemRice, this.transform.position, this.transform.rotation);
        }
        else if(this.name == "MeatBox")
        {
            CurrentOffering = Instantiate(ItemMeat, this.transform.position, this.transform.rotation);
        }
        else
        {
            CurrentOffering = Instantiate(ItemVege, this.transform.position, this.transform.rotation);
        }

        CurrentOffering.GetComponent<PrefabFollower>().SetIsFollow();
        return CurrentOffering;
    }

    public void SetCurrentOfferingToNullAfterGetItem()
    {
        CurrentOffering = null;
    }
}
