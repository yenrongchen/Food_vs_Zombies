using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CookingAreaController : MonoBehaviour
{
    [SerializeField]
    private bool isEmpty = true;

    [SerializeField]
    private bool isWorking = false;

    [SerializeField]
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
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        isWorking = false;

        CurrentOffering = null;

        timeIntervalChop = TIMEINTERVALCHOP;
        timeIntervalCook = TIMEINTERVALCOOK;
        timeIntervalCookRice = TIMEINTERVALCOOKRICE;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking && isEmpty)
        {
            // start operating

            if(this.name == "CookingAreaRice")
            {
                timeIntervalCookRice -= Time.deltaTime;
                if(timeIntervalCookRice <= 0)
                {
                    isWorking = false;
                    isEmpty = false;
                    timeIntervalCookRice = TIMEINTERVALCOOKRICE;
                    CurrentOffering = Instantiate(ItemCookedRice, this.transform.position, this.transform.rotation);
                }
            }
            else if(this.name == "CookingArea")
            {
                timeIntervalCook -= Time.deltaTime;
                if (timeIntervalCook <= 0)
                {
                    isWorking = false;
                    isEmpty = false;
                    timeIntervalCook = TIMEINTERVALCOOK;
                    CurrentOffering = Instantiate(ItemCookedChopped[isAboutMeat], this.transform.position, this.transform.rotation);
                }
            }
            else
            {
                timeIntervalChop -= Time.deltaTime;
                if (timeIntervalChop <= 0)
                {
                    isWorking = false;
                    isEmpty = false;
                    timeIntervalChop = TIMEINTERVALCHOP;
                    CurrentOffering = Instantiate(ItemChopped[isAboutMeat], this.transform.position, this.transform.rotation);
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

    public bool StartWorking(GameObject InputFood)
    {
        if (this.name == "CookingAreaRice")
        {
            if(InputFood.name == "Rice(Clone)")
            {
                isWorking = true;

                return true;
            }
        }
        else if (this.name == "CookingArea")
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
}
