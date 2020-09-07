using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Mushroom", order = 1)]
public class Mushroom : ScriptableObject
{
    public string mushroomName;

    public string description;
    public string realLifeDescription;

    public int id;
    public int value;

    public Sprite sprite;
}
