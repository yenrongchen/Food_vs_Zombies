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

    // Start is called before the first frame update
    void Start()
    {
        isEmpty = false;

        CurrentOffering = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetItem()
    {
        if (isEmpty)
        {
            return null;
        }

        isEmpty = false;

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
