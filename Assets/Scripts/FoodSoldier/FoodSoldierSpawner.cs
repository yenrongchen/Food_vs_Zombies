using UnityEngine;

public class FoodSoldierSpawner : MonoBehaviour
{
    public FoodSoldier foodSoldierPrefab;

    public FoodSoldier Spawn(FoodSoldierData data, Vector3 position, Tile tile)
    {
        FoodSoldier newSoldier = Instantiate(foodSoldierPrefab, position, Quaternion.Euler(0, 180, 0));
        newSoldier.Initialize(data, tile);
        return newSoldier;
    }
}
