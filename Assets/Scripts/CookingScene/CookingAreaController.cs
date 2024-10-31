using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CookingAreaController : MonoBehaviour
{
    private bool isEmpty = true;
    private bool isWorking = false;

    private GameObject CurrentOffering;

    [SerializeField]
    private GameObject ItemCookedRice;

    [SerializeField]
    private GameObject[] ItemCookedChopped;

    [SerializeField]
    private GameObject[] ItemChopped;

    private int isAboutMeat;

    [SerializeField]
    private float TIMEINTERVALCOOKRICE = 8;
    [SerializeField]
    private float TIMEINTERVALCOOK = 8;
    [SerializeField]
    private float TIMEINTERVALCHOP = 8;

    private float timeIntervalCookRice;
    private float timeIntervalCook;
    private float timeIntervalChop;

    [SerializeField]
    private GameObject Canvas;

    private GameObject FatherBar;
    private GameObject activePointer;

    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        isWorking = false;

        CurrentOffering = null;

        FatherBar = null;

        activePointer = this.transform.GetChild(0).gameObject;

        timeIntervalChop = TIMEINTERVALCHOP;
        timeIntervalCook = TIMEINTERVALCOOK;
        timeIntervalCookRice = TIMEINTERVALCOOKRICE;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking && isEmpty)
        {
            // start operating each type of working area

            if(this.name == "CookingAreaRice" || this.name == "CookingAreaRice (1)")  //  pot start working
            {
                if (FatherBar == null)
                {
                    FatherBar = Instantiate(Canvas, this.transform);

                    FatherBar.GetComponentInChildren<ProgressBarController>().SetMaxVal(TIMEINTERVALCOOKRICE);
                }

                timeIntervalCookRice -= Time.deltaTime;
                FatherBar.GetComponentInChildren<ProgressBarController>().SetProgress(timeIntervalCookRice);
                if (timeIntervalCookRice <= 0)
                {
                    isWorking = false;
                    isEmpty = false;
                    timeIntervalCookRice = TIMEINTERVALCOOKRICE;
                    CurrentOffering = Instantiate(ItemCookedRice, this.transform.position, this.transform.rotation);

                    Destroy(FatherBar);
                    FatherBar = null;
                }
            }
            else if(this.name == "CookingArea" || this.name == "CookingArea (1)")  // pan start working
            {
                if (FatherBar == null)
                {
                    FatherBar = Instantiate(Canvas, this.transform);

                    FatherBar.GetComponentInChildren<ProgressBarController>().SetMaxVal(TIMEINTERVALCOOK);
                }

                timeIntervalCook -= Time.deltaTime;
                FatherBar.GetComponentInChildren<ProgressBarController>().SetProgress(timeIntervalCook);
                if (timeIntervalCook <= 0)
                {
                    isWorking = false;
                    isEmpty = false;
                    timeIntervalCook = TIMEINTERVALCOOK;
                    CurrentOffering = Instantiate(ItemCookedChopped[isAboutMeat], this.transform.position, this.transform.rotation);

                    Destroy(FatherBar);
                    FatherBar = null;
                }
            }
            else                                                                // knife start working
            {
                if (FatherBar == null)
                {
                    FatherBar = Instantiate(Canvas, this.transform);

                    FatherBar.GetComponentInChildren<ProgressBarController>().SetMaxVal(TIMEINTERVALCHOP);
                }

                timeIntervalChop -= Time.deltaTime;
                FatherBar.GetComponentInChildren<ProgressBarController>().SetProgress(timeIntervalChop);
                if (timeIntervalChop <= 0)
                {
                    isWorking = false;
                    isEmpty = false;
                    timeIntervalChop = TIMEINTERVALCHOP;
                    CurrentOffering = Instantiate(ItemChopped[isAboutMeat], this.transform.position, this.transform.rotation);

                    Destroy(FatherBar);
                    FatherBar = null;
                }
            }

            if (CurrentOffering != null)
            {
                CurrentOffering.GetComponent<PrefabFollower>().SetIsNotFollow();
                CurrentOffering.transform.position = this.transform.position;
            }
        }
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public bool IsWorking() { 
        return isWorking;
    }

    public bool StartWorking(GameObject InputFood)   //  return true if the working area really takes the food and start working
    {
        if (this.name == "CookingAreaRice" || this.name == "CookingAreaRice (1)")
        {
            if(InputFood.name == "Rice(Clone)")
            {
                isWorking = true;

                return true;
            }
        }
        else if (this.name == "CookingArea" || this.name == "CookingArea (1)")
        {
            if (InputFood.name == "ChoppedMeat(Clone)")
            {
                isWorking = true;
                isAboutMeat = 1;

                return true;
            }
            else if (InputFood.name == "ChoppedVegetable(Clone)")
            {
                isWorking = true;
                isAboutMeat = 0;

                return true;
            }
        }
        else
        {
            if (InputFood.name == "Meat(Clone)")
            {
                isWorking = true;
                isAboutMeat = 1;

                return true;
            }
            else if(InputFood.name == "Vegetable(Clone)")
            {
                isWorking = true;
                isAboutMeat = 0;

                return true;
            }
        }

        return false;
    }

    public GameObject GetItem()
    {
        if(CurrentOffering == null)
        {
            return null;
        }

        CurrentOffering.GetComponent<PrefabFollower>().SetIsFollow();
        isEmpty = true;

        return CurrentOffering;
    }

    public void SetCurrentOfferingToNullAfterGetItem()
    {
        CurrentOffering = null;
    }

    public void SwitchActivePointerState()
    {
        if(activePointer.GetComponent<Renderer>().enabled == true)
        {
            activePointer.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            activePointer.GetComponent<Renderer>().enabled = true;
        }
    }
}
