using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    [SerializeField]
    private GameObject Item;

    [SerializeField]
    private GameObject CurrentItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetSpawnerPos() 
    {  
        return new Vector2(this.transform.position.x, this.transform.position.y); 
    }

    public void SpawnItem(Food currentTaking)
    {
        if(currentTaking == Food.rice)
        {
            CurrentItem = Instantiate(Item, this.transform.position, this.transform.rotation);
        }
    }

    public void DestoryItem()
    {
        Destroy(CurrentItem);
    }
}
