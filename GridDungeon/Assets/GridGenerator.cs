using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int mapHeight = 15;
    public int mapWidth = 15;
    public int totalSquares;

    public GameObject gridSquare;
    bool toggleTiles = false;
    



    // Start is called before the first frame update
    void Start()
    {
        totalSquares = mapHeight * mapWidth;

        GenerateMap();
       

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap()
    {
        Vector2 startPosition = new Vector2(0, 0);
        Color red = Color.red;
        Color blue = Color.blue;

        for (int i = 0; i < mapHeight+1; i++)
        {
            toggleTiles = !toggleTiles;
   

            for (int j = 0; j < mapHeight + 1; j++)
            {
                startPosition = new Vector2(j, i);
                GameObject newGridSquare = Instantiate(gridSquare, startPosition, Quaternion.identity);
                SpriteRenderer spriteRenderer = newGridSquare.GetComponent<SpriteRenderer>();
               

                if(toggleTiles == false)
                {
                    spriteRenderer.color = red;
                   
                }
                else
                {
                    spriteRenderer.color = blue;
                   
                }

                toggleTiles = !toggleTiles;
            }



        }
 
    }




}
