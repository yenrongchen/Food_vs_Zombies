using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        isWorking = false;

        CurrentOffering = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking && isEmpty)
        {
            // start operating

            if(this.name == "CookingAreaRice")
            {
                CurrentOffering = Instantiate(ItemCookedRice, this.transform.position, this.transform.rotation);
            }
            else if(this.name == "CookingArea")
            {
                CurrentOffering = Instantiate(ItemCookedChopped[isAboutMeat], this.transform.position, this.transform.rotation);
            }
            else
            {
                CurrentOffering = Instantiate(ItemChopped[isAboutMeat], this.transform.position, this.transform.rotation);
            }

            CurrentOffering.GetComponent<PrefabFollower>().SetIsNotFollow();
            CurrentOffering.transform.position = this.transform.position;

            isWorking = false;
            isEmpty = false;
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
            print("error in CookingController");
            return null;
        }

        CurrentOffering.GetComponent<PrefabFollower>().SetIsFollow();
        isEmpty = true;

        return CurrentOffering;
    }
}
