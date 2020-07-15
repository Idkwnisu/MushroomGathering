using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BackpackUpgrade", order = 1)]
public class BackpackUpgrade : ScriptableObject
{
    public string upgradeName;

    public int cost;
    public int newSize;
}
