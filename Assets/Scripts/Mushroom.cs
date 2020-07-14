using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Mushroom", order = 1)]
public class Mushroom : ScriptableObject
{
    public string mushroomName;

    public int id;
    public int value;

    public Sprite sprite;
}
