using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExportAreaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ExportDish(GameObject InputDish)   // return true if dish really exist
    {
        switch (InputDish.name)
        {
            case "ChoppedVegetable(Clone)":
                return true;
            case "Sashimi(Clone)":
                return true;
            case "Riceball(Clone)":
                return true;
            case "VegetableMealBox(Clone)":
                return true;
            case "FriedMeatAndVegetable(Clone)":
                return true;
            case "FriedRice(Clone)":
                return true;
        }

        return false;
    }
}
