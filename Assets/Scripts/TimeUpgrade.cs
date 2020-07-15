using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TimeUpgrade", order = 1)]
public class TimeUpgrade : ScriptableObject
{
    public string upgradeName;

    public int cost;
    public int newSeconds;
}
