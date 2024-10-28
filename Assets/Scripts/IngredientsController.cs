using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    [SerializeField]
    private bool isEmpty = false;

    [SerializeField]
    private GameObject ItemRice;

    [SerializeField]
    private GameObject ItemMeat;

    [SerializeField]
    private GameObject ItemVege;

    [SerializeField]
    private GameObject CurrentOffering;

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

        timeIntervalRice = TIMEINTERVALRICE;
        timeIntervalMeat = TIMEINTERVALMEAT;
        timeIntervalVege = TIMEINTERVALVEGE;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty)  //  start count down to respone ingredient
        {
            if (this.name == "RiceBox")
            {
                timeIntervalRice -= Time.deltaTime;
                if(timeIntervalRice <= 0)
                {
                    timeIntervalRice = TIMEINTERVALRICE;
                    isEmpty = false;
                }
            }
            else if (this.name == "MeatBox")
            {
                timeIntervalMeat -= Time.deltaTime;
                if (timeIntervalMeat <= 0)
                {
                    timeIntervalMeat = TIMEINTERVALMEAT;
                    isEmpty = false;
                }
            }
            else
            {
                timeIntervalVege -= Time.deltaTime;
                if (timeIntervalVege <= 0)
                {
                    timeIntervalVege = TIMEINTERVALVEGE;
                    isEmpty = false;
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
}
