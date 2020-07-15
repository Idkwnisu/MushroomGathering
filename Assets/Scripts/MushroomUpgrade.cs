using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MushroomUpgrade", order = 1)]
public class MushroomUpgrade : ScriptableObject
{
    public string upgradeName;

    public int cost;
    public GameObject[] mushrooms;

    public int sizeAugment;
    public int stepsAugment;
}
