using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabFollower : MonoBehaviour
{
    [SerializeField]
    private bool isFollow = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollow == true)
        {
            this.transform.position = GameObject.Find("SpawnPoint").GetComponent<SpawnPointController>().GetSpawnerPos();
        }
    }

    public void SetIsFollow()
    {
        isFollow = true;
    }

    public void SetIsNotFollow()
    {
        isFollow = false;
    }
}
