using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public bool isFreeUp, isFreeDown, isFreeRight, isFreeLeft;

    public NeighbourSquareCollector neighbourSquareCollector;

    private float moveDelay = 2f;

    int maxMovementPoints = 20;
    public int movementPoints;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MakeMovement", moveDelay);
        movementPoints = maxMovementPoints;
    }

    void Update()
    {
        UpdateMovementDirections();
    }

    void UpdateMovementDirections()
    {
        isFreeUp = neighbourSquareCollector.squareIsUp;
        isFreeDown = neighbourSquareCollector.squareIsDown;
        isFreeLeft = neighbourSquareCollector.squareIsLeft;
        isFreeRight = neighbourSquareCollector.squareIsRight;
    }

    void MakeMovement()
    {
        if (movementPoints > 0)
        {

            int randomDirection = Random.Range(0, 4); // Range is 0 to 3 inclusive
            bool moved = false;

            switch (randomDirection)
            {
                case 0:
                    if (isFreeRight)
                    {
                        this.transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
                        moved = true;
                    }
                    break;
                case 1:
                    if (isFreeLeft)
                    {
                        this.transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
                        moved = true;
                    }
                    break;
                case 2:
                    if (isFreeUp)
                    {
                        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
                        moved = true;
                    }
                    break;
                case 3:
                    if (isFreeDown)
                    {
                        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
                        moved = true;
                    }
                    break;
            }

            if (!moved)
            {
                // Schedule another attempt without immediate recursion
                Invoke("MakeMovement", moveDelay);
            }
            else
            {
                // Continue normal movement
                movementPoints -= 1;
                Invoke("MakeMovement", moveDelay);
            }
        }
    }
}

