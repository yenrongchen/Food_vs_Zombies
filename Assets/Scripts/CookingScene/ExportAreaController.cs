using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExportAreaController : MonoBehaviour
{
    [SerializeField] private DragDrop[] _dragDrops;

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
                if (_dragDrops[0] != null) _dragDrops[0].Push();
                return true;
            case "Sashimi(Clone)":
                if (_dragDrops[1] != null) _dragDrops[1].Push();
                return true;
            case "Riceball(Clone)":
                if (_dragDrops[2] != null) _dragDrops[2].Push();
                return true;
            case "VegetableMealBox(Clone)":
                if (_dragDrops[3] != null) _dragDrops[3].Push();
                return true;
            case "FriedMeatAndVegetable(Clone)":
                if (_dragDrops[4] != null) _dragDrops[4].Push();
                return true;
            case "FriedRice(Clone)":
                if (_dragDrops[5] != null) _dragDrops[5].Push();
                return true;
        }

        return false;
    }
}
