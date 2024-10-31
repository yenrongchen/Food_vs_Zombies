using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDishAreaController : MonoBehaviour
{
    private GameObject activePointer;
    // Start is called before the first frame update
    void Start()
    {
        activePointer = this.transform.GetChild(0).gameObject;
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
}
