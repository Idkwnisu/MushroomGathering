using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class TileCreatorAdvanced : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap treeTilemap;
    public Tilemap decorationTilemap;
    public Tile sixTile;
    public Tile fourteenTile;
    public Tile twelveTile;
    public Tile sevenTile;
    public Tile fifteenTile;
    public Tile thirteenTile;
    public Tile threeTile;
    public Tile elevenTile;
    public Tile nineTile;
    public Tile fiveTile;
    public Tile tenTile;
    public Tile oneTile;
    public Tile twoTile;
    public Tile fourTile;
    public Tile eightTile;
    public Tile groundTile;

    public Tile topLeft;
    public Tile topRight;
    public Tile bottomLeft;
    public Tile bottomRight;

    public Tile wallTile;

    public Tile[] decorations;

    private int[,] content;
    private int[,] treeContent;

    public int size;

    public int nrSteps = 30;

    public int nrInits = 1;

    public int smooth = 3;

    public int smoothFactor = 4;

    public float treeGrassLikeness = 0.05f;
    public float treePathLikeness = 0.01f;

    public float decorationLikeness = 0.01f;

    public float lamppostLikeness = 0.01f;

    public float mushroomLikeness = 0.3f;

    public Tile[] treeTile; //temp, it will have a lot of tiles

    public GameObject lamppost;

    public GameObject[] mushrooms;

    public GameObject[] dayAnimals;

    public GameObject[] nightAnimals;

    public float dayAnimalLikeness = 0.02f;

    public float nightAnimalLikeness = 0.02f;

    public GameObject player;


    public Vector2 mushroomRange;


    public float startPercentage = 0.05f;
    public float percentageIncrement = 0.05f;

    public Material groundMaterial;
    public Material decorationsMaterial;
    public Material treesMaterial;

    public bool night = false;

    public Light2D light2D;
    // Start is called before the first frame update
    void Start()
    {
        ReadValues();

        Generate();
        if(night)
        {
            light2D.intensity = 0.2f;
        }
        else
        {
            light2D.intensity = 1.0f;
        }
    }

    public void ReadValues()
    {
        size = GenerationValuesManager.Instance.size;



        nrSteps = GenerationValuesManager.Instance.nrSteps;

        nrInits = GenerationValuesManager.Instance.nrInits;

        smooth = GenerationValuesManager.Instance.smooth;

        smoothFactor = GenerationValuesManager.Instance.smoothFactor;

        treeGrassLikeness = GenerationValuesManager.Instance.treeGrassLikeness;
        treePathLikeness = GenerationValuesManager.Instance.treePathLikeness;

        decorationLikeness = GenerationValuesManager.Instance.decorationLikeness;

        lamppostLikeness = GenerationValuesManager.Instance.lamppostLikeness;

        mushroomLikeness = GenerationValuesManager.Instance.mushroomLikeness;

        MushroomUpgrade currentArea = GenerationValuesManager.Instance.areasAvailable[GenerationValuesManager.Instance.currentAreaActive];

        mushrooms = currentArea.mushrooms;

        decorations = currentArea.tiles;


        groundMaterial.SetFloat("_Range", currentArea.RangeGround);
        groundMaterial.SetFloat("_HueAmount1", currentArea.HueAmount1Ground);
        groundMaterial.SetFloat("_HueAmount2", currentArea.HueAmount2Ground);
        groundMaterial.SetFloat("_SatAmount1", currentArea.SatAmount1Ground);
        groundMaterial.SetFloat("_SatAmount2", currentArea.SatAmount2Ground);
        groundMaterial.SetFloat("_ValueAmount1", currentArea.ValueAmount1Ground);
        groundMaterial.SetFloat("_ValueAmount2", currentArea.ValueAmount2Ground);

        decorationsMaterial.SetFloat("_Range", currentArea.RangeDecorations);
        decorationsMaterial.SetFloat("_HueAmount1", currentArea.HueAmount1Decorations);
        decorationsMaterial.SetFloat("_HueAmount2", currentArea.HueAmount2Decorations);
        decorationsMaterial.SetFloat("_SatAmount1", currentArea.SatAmount1Decorations);
        decorationsMaterial.SetFloat("_SatAmount2", currentArea.SatAmount2Decorations);
        decorationsMaterial.SetFloat("_ValueAmount1", currentArea.ValueAmount1Decorations);
        decorationsMaterial.SetFloat("_ValueAmount2", currentArea.ValueAmount2Decorations);

        treesMaterial.SetFloat("_HueAmount1", currentArea.HueAmount1Trees);
        treesMaterial.SetFloat("_HueAmount2", currentArea.HueAmount2Trees);
        treesMaterial.SetFloat("_SatAmount1", currentArea.SatAmount1Trees);
        treesMaterial.SetFloat("_SatAmount2", currentArea.SatAmount2Trees);
        treesMaterial.SetFloat("_ValueAmount1", currentArea.ValueAmount1Trees);
        treesMaterial.SetFloat("_ValueAmount2", currentArea.ValueAmount2Trees);


        startPercentage = GenerationValuesManager.Instance.startPercentage;
        percentageIncrement = GenerationValuesManager.Instance.percentageIncrement;

        night = GenerationValuesManager.Instance.night;
}

    public void Generate()
    {
        content = new int[size, size];
        treeContent = new int[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                content[x, y] = 0;
                treeContent[x, y] = 0;
            }
        }


        for (int i = 0; i < nrInits; i++)
        {
            InitMatrix();

        }

        for (int i = 0; i < smooth; i++)
        {
            Smooth();
        }

        Ground();

        Trees();
        DrawMap();
    }

    public void InitMatrix()
    {
        int tileNrX = Random.Range(0, size);
        int tileNrY = Random.Range(0, size);

        content[tileNrX, tileNrY] = 1;

        StepsDwarf(nrSteps, tileNrX, tileNrY);
    }

    public void StepsRandomWalk(int nrSteps, int xStartPos, int yStartPos)
    {
        for (int i = 0; i < nrSteps; i++)
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    xStartPos++;
                }
                else
                {
                    xStartPos--;
                }
            }
            else
            {
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    yStartPos++;
                }
                else
                {
                    yStartPos--;
                }
            }

            if (xStartPos < 0)
            {
                xStartPos = 0;
            }
            if (xStartPos >= size)
            {
                xStartPos = size - 1;
            }

            if (yStartPos < 0)
            {
                yStartPos = 0;
            }
            if (yStartPos >= size)
            {
                yStartPos = size - 1;
            }

                content[xStartPos, yStartPos] = 1;
        }
    }

    public void DrawMap()
    {
        bool playerPositioned = false;
        for (int x = 0; x < size; x++)
        {
            for (int y = size - 1; y >= 0; y--)
            {
                if (content[x, y] == 1)
                {

                    if (x > 0 && y > 0 && x < size - 1 && y < size - 1)
                    {
                        int one = content[x, y + 1];
                        int two = content[x + 1, y];
                        int four = content[x, y - 1];
                        int eight = content[x - 1, y];

                        int result = one + 2 * two + 4 * four + 8 * eight;
                        switch (result)
                        {
                            case 1:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), oneTile);
                                break;
                            case 2:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), twoTile);
                                break;
                            case 3:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), threeTile);
                                break;
                            case 4:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), fourTile);
                                break;
                            case 5:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), fiveTile);
                                break;
                            case 6:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), sixTile);
                                break;
                            case 7:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), sevenTile);
                                break;
                            case 8:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), eightTile);
                                break;
                            case 9:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), nineTile);
                                break;
                            case 10:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), tenTile);
                                break;
                            case 11:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), elevenTile);
                                break;
                            case 12:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), twelveTile);
                                break;
                            case 13:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), thirteenTile);
                                break;
                            case 14:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), fourteenTile);
                                break;
                            default:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), fifteenTile);
                                break;
                        }
                        if (result == 15)//empty
                        {
                            if (content[x - 1, y - 1] == 0)
                            {
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), topLeft);
                            }
                            else if (content[x - 1, y + 1] == 0)
                            {
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), bottomLeft);
                            }
                            else if (content[x + 1, y - 1] == 0)
                            {
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), topRight);
                            }
                            else if (content[x + 1, y + 1] == 0)
                            {
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), bottomRight);
                            }
                        }

                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), fifteenTile);
                    }

                    if (!night) //animals
                    {
                        if (Random.Range(0.0f, 1.0f) < dayAnimalLikeness)
                        {
                            int nrAnimal = Random.Range(0, dayAnimals.Length);
                            GameObject animal = Instantiate(dayAnimals[nrAnimal], tilemap.CellToWorld(new Vector3Int(x - size / 2, y - size / 2, 0)), Quaternion.identity);
                        }
                    }
                    else
                    {
                        if (Random.Range(0.0f, 1.0f) < nightAnimalLikeness)
                        {
                            int nrAnimal = Random.Range(0, nightAnimals.Length);
                            GameObject animal = Instantiate(nightAnimals[nrAnimal], tilemap.CellToWorld(new Vector3Int(x - size / 2, y - size / 2, 0)), Quaternion.identity);
                        }
                    }

                    if (!playerPositioned)
                    {
                        player.transform.position = tilemap.CellToWorld(new Vector3Int(x - size / 2, y - size / 2, 0));
                        playerPositioned = true;
                    }
                }
                else if (content[x, y] == 2)
                    tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), wallTile);
                else
                {
                    tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), groundTile);
                }

                //Trees
                if(treeContent[x, y] == 1)
                {
                    if (Random.Range(0.0f, 1.0f) < mushroomLikeness)
                    {
                        int mush = Random.Range(0, mushrooms.Length);
                        Vector3 pos = treeTilemap.CellToWorld(new Vector3Int(x - size / 2, y - size/2,0));
                        pos += new Vector3(Random.Range(-mushroomRange.x, mushroomRange.x), Random.Range(-mushroomRange.y, mushroomRange.y), 0.0f);
                        GameObject mushroom = Instantiate(mushrooms[mush], pos, Quaternion.identity);
                    }
                        int count = 0;
                    for(int i = 0; i < 3; i ++)
                    {
                        for(int z = 0; z < 5; z++) //foliage
                        {
                            Vector3Int position = new Vector3Int(x - size / 2 - 2 + z, y - size / 2 + 4 - i, 0);
                            treeTilemap.SetTile(position, treeTile[count]);
                            
                           // tilemap.SetColliderType(position, Tile.ColliderType.Grid);
                           count++;
                        }
                    }
                    for(int i = 0; i < 2; i++)
                    {
                        for(int z = 0; z < 3; z++) //stump
                        {
                            Vector3Int position = new Vector3Int(x - size / 2 - 1 + z, y - size / 2 + 1 - i, 0);
                            treeTilemap.SetTile(position, treeTile[count]);
                            
                            // tilemap.SetColliderType(position, Tile.ColliderType.Grid);
                            count++;
                        }
                    }
                    
                }
                else if(treeContent[x, y] > 1)//decoration
                {
                    decorationTilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), decorations[treeContent[x,y]-2]);
                }
                else if(treeContent[x, y] == -1)
                {
                    GameObject lamp = Instantiate(lamppost, treeTilemap.CellToWorld(new Vector3Int(x - size / 2, y - size / 2, 0)), Quaternion.identity);
                    if (night)
                    {
                        lamp.GetComponent<Lamppost>().Night();
                    }
                    else
                    {
                        lamp.GetComponent<Lamppost>().Day();
                    }
                }
            }
        }
    }

    public void StepsDwarf(int nrSteps, int xStartPos, int yStartPos)
    {
        Vector2Int direction = new Vector2Int();
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                direction.x = 1;
                direction.y = 0;
            }
            else
            {
                direction.x = -1;
                direction.y = 0;
            }
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                direction.x = 0;
                direction.y = 1;
            }
            else
            {
                direction.x = 0;
                direction.y = -1;
            }
        }
        float percentage = startPercentage;
        for (int i = 0; i < nrSteps; i++)
        {
            xStartPos += direction.x;
            yStartPos += direction.y;

            if (Random.Range(0.0f, 1.0f) < percentage)
            {
                
                if(Mathf.Abs(direction.x) > 0)
                {
                    direction.x = 0;
                    if(Random.Range(0.0f,1.0f) > 0.5f)
                    {
                        direction.y = 1;
                    }
                    else
                    {
                        direction.y = -1;
                    }
                }
                else //the y is more than 0
                {
                    direction.y = 0;
                    if (Random.Range(0.0f, 1.0f) > 0.5f)
                    {
                        direction.x = 1;
                    }
                    else
                    {
                        direction.x = -1;
                    }
                }
                percentage = startPercentage;
            }
            else
            {
                percentage += percentageIncrement;
            }


            if (xStartPos < 0)
            {
                xStartPos = 0;
                direction.x *= -1;
            }
            if (xStartPos >= size)
            {
                xStartPos = size - 1;
                direction.x *= -1;
            }

            if (yStartPos < 0)
            {
                yStartPos = 0;
                direction.y *= -1;
            }
            if (yStartPos >= size)
            {
                yStartPos = size - 1;
                direction.y *= -1;
            }

            content[xStartPos, yStartPos] = 1;
        }
            
        
    }

    public void Ground()
    {
        int[,] backup = (int[,])content.Clone();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (x > 0 && y > 0 && x < size - 1 && y < size - 1)
                {
                    int acc = 0;
                    for (int nX = -1; nX < 2; nX++)
                    {
                        for (int nY = -1; nY < 2; nY++)
                        {
                            if (nX != 0 || nY != 0)
                                acc += backup[x + nX, y + nY];
                        }
                    }
                    if (acc == 0)
                    {
                        content[x, y] = 2;
                    }

                }
                else
                {
                    content[x, y] = 2;
                }
            }
        }
    }

    public void Smooth()
    {
        int[,] backup = (int[,])content.Clone();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if(x > 0 && y > 0 && x < size-1 && y < size-1)
                {
                    int acc = 0;
                    for(int nX = -1; nX < 2; nX++)
                    {
                        for (int nY = -1; nY < 2; nY++)
                        {
                            if (nX != 0 || nY != 0)
                                acc += backup[x + nX, y + nY];
                        }
                    }
                    if(acc >= smoothFactor)
                    {
                        content[x, y] = 1;
                    }

                }
            }
        }
    }

    public void Trees()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                bool availableToDecor = false;
                bool occupied = false;

                for (int i = -7; i < 7; i++)
                {
                    for (int z = -7; z < 7; z++)
                    {
                        if (x + i > 0 && x + i < size && y + z > 0 && y + z < size)
                        {
                            if (x + i >= 0 && x + i < size && y + z >= 0 && y + z < size)
                            {
                                if (treeContent[x + i, y + z] == 1 || treeContent[x + i, y + z] == -1)
                                    occupied = true;
                            }
                        }
                    }
                }

                if (content[x,y] == 0 || content[x,y] == 1)
                {
                    
                    
                    if (!occupied)
                    {
                        if (Random.Range(0.0f, 1.0f) < lamppostLikeness)
                        {
                            treeContent[x, y] = -1;
                        }
                        else
                        {
                            if (content[x, y] == 0)
                            {
                                if (Random.Range(0.0f, 1.0f) < treeGrassLikeness)
                                {
                                    treeContent[x, y] = 1;
                                }
                                else
                                {
                                    availableToDecor = true;
                                }
                            }
                            if (content[x, y] == 1)
                            {
                                if (Random.Range(0.0f, 1.0f) < treePathLikeness)
                                {
                                    treeContent[x, y] = 1;
                                }
                            }

                            
                        }
                    }
                }
                else if (content[x, y] == 2 && !occupied)
                {
                    availableToDecor = true;
                }

                if (availableToDecor)
                {
                    if(Random.Range(0.0f, 1.0f) < decorationLikeness)
                    {
                        treeContent[x, y] = Random.Range(0, decorations.Length) + 2;
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            for (int x = -5; x < size + 5; x++)
            {
                for (int y = -5; y < size + 5; y++)
                {
                    treeTilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), null);

                }
            }
            Generate();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Smooth();
            DrawMap();
        }
    }
}
