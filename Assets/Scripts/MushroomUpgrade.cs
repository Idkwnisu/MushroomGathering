using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MushroomUpgrade", order = 1)]
public class MushroomUpgrade : ScriptableObject
{
    public string upgradeName;

    public int cost;
    public GameObject[] mushrooms;
    public Tile[] tiles;
    public int[] tileProb;
    public GameObject[] bigDecorations;
    public int[] decorationProb;

    public GameObject[] toIstantiateOnPlayer;
    public GameObject[] toInstantiateOnScene;

    public GameObject lamppost;

    public GameObject[] dayAnimals;

    public GameObject[] nightAnimals;

    public float RangeGround = 0.14f;

    public float RangeDecorations = 0.14f;

    public float HueAmount1Ground;
    public float SatAmount1Ground;
    public float ValueAmount1Ground;
    public float HueAmount2Ground;
    public float SatAmount2Ground;
    public float ValueAmount2Ground;


    public float HueAmount1Decorations;
    public float SatAmount1Decorations;
    public float ValueAmount1Decorations;
    public float HueAmount2Decorations;
    public float SatAmount2Decorations;
    public float ValueAmount2Decorations;


    public float HueAmount1Trees;
    public float SatAmount1Trees;
    public float ValueAmount1Trees;
    public float HueAmount2Trees;
    public float SatAmount2Trees;
    public float ValueAmount2Trees;

    public float treeGrassLikeness = 0.05f;
    public float treePathLikeness = 0.01f;

    public float decorationLikeness = 0.1f;
    public float bigDecorationLikeness = 0.03f;

    public float lamppostLikeness = 0.01f;
}
