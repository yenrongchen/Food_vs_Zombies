using UnityEngine;

// stats of foods
[System.Serializable]
public class FoodSoldierStat
{
    public float hp;
    public float atk;
    public float atkSpeed;
    public bool penetration;
}

// data container for stats of foods
// create in Project menu (stored in Assets/Stats/ )
[CreateAssetMenu(fileName = "FoodSoldierStat", menuName = "FoodSoldier/Stats", order = 1)]
public class FoodSoldierData : ScriptableObject
{
    public FoodSoldierStat stat;
    public RuntimeAnimatorController animatorController;
}
