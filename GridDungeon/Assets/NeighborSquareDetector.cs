using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborSquareDetector : MonoBehaviour
{
    public bool hasNeighbourSquare;
    SpriteRenderer spriteRenderer;
    public Color red = Color.red;
    public Color green = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grid"))
        {
            hasNeighbourSquare = true;
            spriteRenderer.color = green;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grid"))
        {
            hasNeighbourSquare = false;
            spriteRenderer.color = red;
        }

    }
}
