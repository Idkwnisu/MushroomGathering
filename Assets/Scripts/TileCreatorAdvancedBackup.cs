using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreatorAdvancedBackup : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile sixTile;
    public Tile fourteenTile;
    public Tile twelveTile;
    public Tile sevenTile;
    public Tile fifteenTile;
    public Tile thirteenTile;
    public Tile threeTile;
    public Tile elevenTile;
    public Tile nineTile;
    public Tile groundTile;

    public Tile wallTile;

    private int[,] content;

    public int size;

    public int nrSteps = 30;

    public int nrInits = 1;

    public int smooth = 3;

    public int smoothFactor = 4;


    public float startPercentage = 0.05f;
    public float percentageIncrement = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }
    public void Generate()
    {
        content = new int[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                content[x, y] = 0;
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

            Debug.Log(xStartPos);
            Debug.Log(yStartPos);

            content[xStartPos, yStartPos] = 1;
        }
    }

    public void DrawMap()
    {

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
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
                            case 3:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), threeTile);
                                break;
                            case 6:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), sixTile);
                                break;
                            case 7:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), sevenTile);
                                break;
                            case 9:
                                tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), nineTile);
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
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), fifteenTile);
                    }
                }
                else if (content[x, y] == 2)
                    tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), wallTile);
                else
                    tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), groundTile);
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

                if (Mathf.Abs(direction.x) > 0)
                {
                    direction.x = 0;
                    if (Random.Range(0.0f, 1.0f) > 0.5f)
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
                    if (acc >= smoothFactor)
                    {
                        content[x, y] = 1;
                    }

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Smooth();
            DrawMap();
        }
    }
}
