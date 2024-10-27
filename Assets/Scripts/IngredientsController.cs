using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    [SerializeField]
    private bool isEmpty = false;
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print((int)Food.meat);
    }

    public Food GetItem()
    {
        if (isEmpty)
        {
            return Food.none;
        }

        isEmpty = true;

        if(this.name == "RiceBox")
        {
            return Food.rice;
        }
        else if(this.name == "MeatBox")
        {
            return Food.meat;
        }
        else
        {
            return Food.vege;
        }
    }
}
