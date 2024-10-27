using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingAreaRiceController : MonoBehaviour
{
    [SerializeField]
    private bool isEmpty = true;

    [SerializeField]
    private bool isWorking = false;
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        isWorking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public bool IsWorking() { 
        return isWorking;
    }

    public void StartWorking()
    {
        isWorking = true;
    }
}
