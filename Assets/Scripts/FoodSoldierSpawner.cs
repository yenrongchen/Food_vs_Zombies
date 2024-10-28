using UnityEngine;

public class FoodSoldierSpawner : MonoBehaviour
{
    public FoodSoldier foodSoldierPrefab;

    // FoodSoldierData are stored in Asset/Stats/
    public FoodSoldierData salad;
    public FoodSoldierData sashimi;
    public FoodSoldierData riceball;
    public FoodSoldierData bento;
    public FoodSoldierData stirfry;
    public FoodSoldierData friedrice;

    public void Spawn(FoodSoldierData stat)
    {
        FoodSoldier newSoldier = Instantiate(foodSoldierPrefab);
        newSoldier.Initialize(stat);
    }

    private void Start()
    {
        // e.g. spawn a Sashimi
        Spawn(sashimi);
    }


}
