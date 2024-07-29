using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ZombieMovement : MonoBehaviour
{
    public bool isFreeUp, isFreeDown, isFreeRight, isFreeLeft;

    public NeighbourSquareCollector neighbourSquareCollector;


    private float moveDelay = 1f;

    int maxMovementPoints = 4;
    public int movementPoints;
    public TMP_Text movementPointsText;
    public GameObject zombieSprite;


    public TurnManager turnManager;
    public bool movePhase;
    public bool moved;

    public bool isOnPreviousMove;

    public GameObject prevMoveSprite;
    GameObject prevMovement;

    public bool isOnScreen;

    public delegate void ZombieCompletedTurn();
    public static event ZombieCompletedTurn OnZombieCompletedTurn;

    private void OnEnable()
    {
        TurnManager.OnZombieMove += StartMovePhase;
    }
    private void OnDisable()
    {
        TurnManager.OnZombieMove -= StartMovePhase;
    }
    // Start is called before the first frame update
    void Start()
    {
        movementPoints = maxMovementPoints;
       
    }




        void Update()
        {
            UpdateMovementDirections();

            if (movementPoints <= 0 && movePhase)
            {
                StopMovePhase();
                if (OnZombieCompletedTurn != null)
                {
                    OnZombieCompletedTurn();
                }
            }




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
        Debug.Log("Enter Movement");
        if (movePhase)
        {

            Debug.Log("Enter Move Allowed");
            if (movementPoints > 0)
            {
                Debug.Log("Enter Move Points Valid");
                moved = false;

                

                int randomDirection = Random.Range(0, 4); // Range is 0 to 3 inclusive
               

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
                    MakeMovement();
                    }
                    else
                    {
                    // Continue normal movement
                    Instantiate(prevMoveSprite, zombieSprite.transform.position, Quaternion.identity);
                    movementPoints -= 1;
                    UpdateMovementPoints();

                        if (movementPoints > 0)
                        {
                            Invoke("MakeMovement", moveDelay);
                        }
                     
                        
                    }
            }

        }
        
    }
    
    public void StartMovePhase()
    {
        Debug.Log("ZOMBIE TO MOVE!");
        ResetMovePoints();
        movePhase = true;
        Invoke("MakeMovement", moveDelay);
    }

    public void StopMovePhase()
    {
        turnManager.SwitchTurn("Player", "Move");
        movePhase = false;
    }

    public void ResetMovePoints()
    {
        movementPoints = maxMovementPoints;
        UpdateMovementPoints();
    }

    public void UpdateMovementPoints()
    {
        movementPointsText.text = movementPoints.ToString();
    }
}

