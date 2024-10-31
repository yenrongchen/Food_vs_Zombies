using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// for numerical usage, save time by using array
public enum Food
{
    cookedRice,
    choppedMeat,
    choppedVege,
    cookedChoppedMeat,
    cookedChoppedVege,
    sashimi,
    riceBall,
    vegeMealBox,
    friedMeatVege,
    friedRice
}

public class CombineAreaController : MonoBehaviour
{
    // declare some constant
    const int SUCCESS_BUT_DONT_DESTORY = 1;
    const int FAILED = 0;
    const int SUCCESS = 2;
    const int DISTANCE = 5;

    private int[] currentFoodOnTable = new int[5];

    private GameObject CurrentOffering;

    [SerializeField]
    private GameObject[] ItemDishes = new GameObject[6];

    private bool changed;

    private GameObject activePointer;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < currentFoodOnTable.Length; i++) 
        { 
            currentFoodOnTable[i] = 0;
        }

        CurrentOffering = null;
        changed = false;
        activePointer = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // return 0 if refuse to take in the food, 1 if take origin(transfer the ownership), 2 if to generate a new one and destroy the origin
    // key points is to memo the current ingredients or food on the combine table
    // base on the memo to tell if the player can put the food in
    public int StartWorking(GameObject InputFood)
    {
        switch (InputFood.name)
        {
            case "CookedRice(Clone)":
                if (InputCookedRiceCheck())
                {
                    currentFoodOnTable[(int)Food.cookedRice] = 1;

                    changed = true;
                }

                break;
            case "ChoppedMeat(Clone)":
                if (InputChoppedMeatCheck())
                {
                    currentFoodOnTable[(int)Food.choppedMeat] = 1;

                    changed = true;
                }

                break;
            case "ChoppedVegetable(Clone)":
                if (InputChoppedVegeCheck())
                {
                    currentFoodOnTable[(int)Food.choppedVege] = 1;

                    changed = true;  
                }

                break;
            case "CookedChoppedMeat(Clone)":
                if (InputCookedChoppedMeatCheck())
                {
                    currentFoodOnTable[(int)Food.cookedChoppedMeat] = 1;

                    changed = true;
                }

                break;
            case "CookedChoppedVegetable(Clone)":
                if (InputCookedChoppedVegeCheck())
                {
                    currentFoodOnTable[(int)Food.cookedChoppedVege] = 1;

                    changed = true;
                }

                break;
            case "Sashimi(Clone)":
                if (InputChoppedMeatCheck() && InputChoppedVegeCheck())
                {
                    currentFoodOnTable[(int)Food.choppedMeat] = 1;
                    currentFoodOnTable[(int)Food.choppedVege] = 1;

                    changed = true;
                }

                break;
            case "Riceball(Clone)":
                if (InputCookedRiceCheck() && InputCookedChoppedMeatCheck())
                {
                    currentFoodOnTable[(int)Food.cookedChoppedMeat] = 1;
                    currentFoodOnTable[(int)Food.cookedRice] = 1;

                    changed = true;
                }

                break;
            case "VegetableMealBox(Clone)":
                if (InputCookedRiceCheck() && InputCookedChoppedVegeCheck())
                {
                    currentFoodOnTable[(int)Food.cookedChoppedVege] = 1;
                    currentFoodOnTable[(int)Food.cookedRice] = 1;

                    changed = true;
                }

                break;
            case "FriedMeatAndVegetable(Clone)":
                if (InputCookedChoppedVegeCheck() && InputCookedChoppedMeatCheck())
                {
                    currentFoodOnTable[(int)Food.cookedChoppedMeat] = 1;
                    currentFoodOnTable[(int)Food.cookedChoppedVege] = 1;

                    changed = true;
                }

                break;
            case "FriedRice(Clone)":
                if (InputCookedChoppedVegeCheck() && InputCookedChoppedMeatCheck() && InputCookedRiceCheck())
                {
                    currentFoodOnTable[(int)Food.cookedChoppedMeat] = 1;
                    currentFoodOnTable[(int)Food.cookedChoppedVege] = 1;
                    currentFoodOnTable[(int)Food.cookedRice] = 1;

                    changed = true;
                }

                break;
        }

        if (changed)
        { 
            changed = false;

            return Refresh(InputFood);
        }
        else
        {
            return FAILED;
        }
    }

    // after we made amemo, we need to update the outcome dish
    private int Refresh(GameObject InputFood)
    {
        if(CurrentOffering != null)
        {
            Destroy(CurrentOffering);
            CurrentOffering = null;
        }

        int numOfFood = 0;
        int codeToDish = 0;
        for (int i = 0; i < currentFoodOnTable.Length; i++)
        {
            if (currentFoodOnTable[i] == 1)
            {
                numOfFood ++;
                codeToDish += ((i + 1) * (i + 1));    // just to make codeToDish unique so we can use it to get the specific dish faster
            }
        }

        if(numOfFood == 1)                           // no need to create a new type of dish, just use the original one given by player
        {
            InputFood.GetComponent<PrefabFollower>().SetIsNotFollow();
            InputFood.transform.position = this.transform.position;
            CurrentOffering = InputFood;

            return SUCCESS_BUT_DONT_DESTORY;
        }

        switch (codeToDish)
        {
            case 13:
                CurrentOffering = Instantiate(ItemDishes[(int)Food.sashimi - DISTANCE], this.transform.position, this.transform.rotation);
                break;
            case 17:
                CurrentOffering = Instantiate(ItemDishes[(int)Food.riceBall - DISTANCE], this.transform.position, this.transform.rotation);
                break;
            case 26:
                CurrentOffering = Instantiate(ItemDishes[(int)Food.vegeMealBox - DISTANCE], this.transform.position, this.transform.rotation);
                break;
            case 41:
                CurrentOffering = Instantiate(ItemDishes[(int)Food.friedMeatVege - DISTANCE], this.transform.position, this.transform.rotation);
                break;
            case 42:
                CurrentOffering = Instantiate(ItemDishes[(int)Food.friedRice - DISTANCE], this.transform.position, this.transform.rotation);
                break;
        }

        CurrentOffering.GetComponent<PrefabFollower>().SetIsNotFollow();
        CurrentOffering.transform.position = this.transform.position;

        return SUCCESS;
    }

    public GameObject GetItem()
    {
        if (CurrentOffering == null)
        {
            return null;
        }

        CurrentOffering.GetComponent<PrefabFollower>().SetIsFollow();
        for (int i = 0; i < currentFoodOnTable.Length; i++)
        {
            currentFoodOnTable[i] = 0;
        }


        return CurrentOffering;
    }

    public void SetCurrentOfferingToNullAfterGetItem()
    {
        CurrentOffering = null;
    }

    public void SwitchActivePointerState()
    {
        if (activePointer.GetComponent<Renderer>().enabled == true)
        {
            activePointer.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            activePointer.GetComponent<Renderer>().enabled = true;
        }
    }

    // all the checkers to tell if the player can put a specific food in the current combine table
    private bool InputCookedRiceCheck()
    {
        if (currentFoodOnTable[(int)Food.cookedRice] == 0 && currentFoodOnTable[(int)Food.choppedMeat] == 0
            && currentFoodOnTable[(int)Food.choppedVege] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool InputChoppedMeatCheck()
    {
        if (currentFoodOnTable[(int)Food.choppedMeat] == 0 && currentFoodOnTable[(int)Food.cookedChoppedMeat] == 0
            && currentFoodOnTable[(int)Food.cookedChoppedVege] == 0 && currentFoodOnTable[(int)Food.cookedRice] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool InputChoppedVegeCheck()
    {
        if (currentFoodOnTable[(int)Food.choppedVege] == 0 && currentFoodOnTable[(int)Food.cookedChoppedMeat] == 0
            && currentFoodOnTable[(int)Food.cookedChoppedVege] == 0 && currentFoodOnTable[(int)Food.cookedRice] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool InputCookedChoppedMeatCheck()
    {
        if (currentFoodOnTable[(int)Food.cookedChoppedMeat] == 0 && currentFoodOnTable[(int)Food.choppedMeat] == 0
            && currentFoodOnTable[(int)Food.choppedVege] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool InputCookedChoppedVegeCheck()
    {
        if (currentFoodOnTable[(int)Food.cookedChoppedVege] == 0 && currentFoodOnTable[(int)Food.choppedMeat] == 0
            && currentFoodOnTable[(int)Food.choppedVege] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
