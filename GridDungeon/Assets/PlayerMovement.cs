using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject playerSprite;
    public PlayerInput playerInput;
    InputAction moveAction;

    public int maxPlayerMovePoints = 10;
    public int playerMovePoints;

    public int maxPlayerAttackPoints = 2;
    public int playerAttackPoints;

    public Vector2 playerPosition;

    public bool isFreeUp, isFreeDown, isFreeRight, isFreeLeft;

    public NeighbourSquareCollector neighbourSquareCollector;

    public TMP_Text movementPointsText, attackPointsText;
    public AudioClip[] footSteps;
    AudioSource audioSource;

    public TurnManager turnManager;
    public bool movePhase;
    public bool attackPhase;

    public Transform centralAttackCursorPosition;
    public GameObject attackCursor;
    public GameObject attackHitbox;

    public int attackRange = 1;

    public List<Transform> enemyPositions;

    public GameObject tempHitSprite;

    

    private void OnEnable()
    {
        TurnManager.OnPlayerMove += StartMovePhase;
        TurnManager.OnPlayerAttack += StartAttackPhase;
    }
    private void OnDisable()
    {
        TurnManager.OnPlayerMove -= StartMovePhase;
        TurnManager.OnPlayerAttack -= StartAttackPhase;
    }

    // Start is called before the first frame update
    void Start()
    {
       

        moveAction = playerInput.actions.FindAction("Move");
        playerPosition = player.transform.position;
       
        audioSource = GetComponent<AudioSource>();

        UpdatePlayerMovePointText();
        UpdatePlayerAttackPointText();

    }


    // Update is called once per frame
    void Update()
    {

        // Check Neighbour Squares are Free or not
        #region Neighbour Square Check

        if (neighbourSquareCollector.squareIsUp)
        {
            isFreeUp = true;
        }
        else { isFreeUp = false; }

        if (neighbourSquareCollector.squareIsDown)
        {
            isFreeDown = true;
        }
        else { isFreeDown = false; }

        if (neighbourSquareCollector.squareIsLeft)
        {
            isFreeLeft = true;
        }
        else { isFreeLeft = false; }

        if (neighbourSquareCollector.squareIsRight)
        {
            isFreeRight = true;
        }
        else { isFreeRight = false; }
        #endregion


        if (playerMovePoints <= 0 && movePhase)
        {
            StopMovePhase();
        }
        
        if (playerAttackPoints <= 0 && attackPhase)
        {
            StopAttackPhase();
        }
                

        if (attackPhase)
        {
            attackCursor.gameObject.SetActive(true);
            attackHitbox.gameObject.SetActive(true);
        }
        else
        {
            attackCursor.gameObject.SetActive(false);
            attackHitbox.gameObject.SetActive(false);
        }



    }

    public void UpdateEnemyPositions()
    {
        enemyPositions = new List<Transform>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");


        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPositions.Add(enemies[i].transform);
            Debug.Log("Position Enemy " + i + ": " + enemyPositions[i].position);
        }

    }


    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        { }

        else if (context.performed)
        {
            if (movePhase)
            {

                if (playerMovePoints > 0)
                {
                    Vector2 moveValue = moveAction.ReadValue<Vector2>();

                    #region Normalize input Move Value

                    moveValue = NormalizInput(moveValue);

                    #region Previous, Working Individual Normalize Values, Pre Function Switching
                    /*
                    if (moveValue.x > 0)
                    {
                        moveValue.x = 1;
                        playerSprite.transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (moveValue.x < 0)
                    {
                        moveValue.x = -1;
                        playerSprite.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (moveValue.y > 0)
                    {
                        moveValue.y = 1;
                    }
                    if (moveValue.y < 0)
                    {
                        moveValue.y = -1;
                    }*/
                    #endregion
                    #endregion

                    #region Account for Diagonal Moves
                    moveValue = DiagonalBlock(moveValue);
                    /* // Previous, Working Individual Diagonal Block, Pre Function Switching
                    if (moveValue.x > 0 && moveValue.y > 0 ||
                            moveValue.x < 0 && moveValue.y < 0 ||
                            moveValue.x > 0 && moveValue.y < 0 ||
                            moveValue.x < 0 && moveValue.y > 0)
                    {
                        moveValue = Vector2.zero;
                    }*/
                    #endregion


                    #region Read Input and Move Accordingly, subtract move points per move

                    if (moveValue.x > 0 && isFreeRight)
                    {
                        PickRandomFootStep();
                        this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                        playerMovePoints -= 1;

                    }

                    else if (moveValue.x < 0 && isFreeLeft)
                    {
                        PickRandomFootStep();
                        this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                        playerMovePoints -= 1;
                    }
                    else if (moveValue.y > 0 && isFreeUp)
                    {
                        PickRandomFootStep();
                        this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                        playerMovePoints -= 1;
                    }
                    else if (moveValue.y < 0 && isFreeDown)
                    {
                        PickRandomFootStep();
                        this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                        playerMovePoints -= 1;
                    }
                    #endregion





                }

            }
            else if (attackPhase)
            {
                if (playerAttackPoints > 0)
                { 
                    Debug.Log("Enter Attack Phase");

                    Vector2 attackValue = moveAction.ReadValue<Vector2>();


                    #region Normalize input Attack Value

                    attackValue = NormalizInput(attackValue);

                    #region Previous, Working Individual Normalize Values, Pre Function Switching
                    /*
                    if (attackValue.x > 0)
                    {
                        attackValue.x = 1;
                    }
                    if(attackValue.x < 0)
                    {
                        attackValue.x = -1;
                    }
                    if(attackValue.y > 0)
                    {
                        attackValue.y = 1;
                    }
                    if(attackValue.y < 0)
                    {
                        attackValue.y = -1;
                    }*/
                    #endregion
                    #endregion

                    #region Account for Diagonal Attack
                    attackValue = DiagonalBlock(attackValue);
                    #endregion

                    // Place Attack Cursor
                    if (attackValue.x > 0)
                    {
                        attackCursor.transform.position = new Vector2(centralAttackCursorPosition.position.x + 1, centralAttackCursorPosition.position.y);

                        CheckEnemyInRange(new Vector2(attackCursor.transform.position.x - 0.5f, attackCursor.transform.position.y - 0.5f));
                    
                    }
                    else if ( attackValue.x < 0)
                    {
                        attackCursor.transform.position = new Vector2(centralAttackCursorPosition.position.x - 1, centralAttackCursorPosition.position.y);
                        CheckEnemyInRange(new Vector2(attackCursor.transform.position.x - 0.5f, attackCursor.transform.position.y - 0.5f));
                    }
                    else if (attackValue.y > 0)
                    {
                        attackCursor.transform.position = new Vector2(centralAttackCursorPosition.position.x, centralAttackCursorPosition.position.y + 1);
                        CheckEnemyInRange(new Vector2(attackCursor.transform.position.x - 0.5f, attackCursor.transform.position.y - 0.5f));
                    }
                    else if (attackValue.y < 0)
                    {
                        attackCursor.transform.position = new Vector2(centralAttackCursorPosition.position.x, centralAttackCursorPosition.position.y - 1);
                        CheckEnemyInRange(new Vector2(attackCursor.transform.position.x - 0.5f, attackCursor.transform.position.y - 0.5f));
                    }
                    else
                    {
                        attackCursor.transform.position = centralAttackCursorPosition.transform.position;
                    }

                    attackHitbox.transform.position = attackCursor.transform.position;
                }
                else
                {

                }
            }

            UpdatePlayerMovePointText();
            UpdatePlayerAttackPointText();

        }
        else if (context.canceled) // If at the end of move, player is out of points, trigger end of Move Phase, go to Enemy Phase
        {

        }

       

       

    }

    public void CheckEnemyInRange(Vector2 attackSquare)
    {
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            Debug.Log("Square: " + attackSquare);

            if (new Vector2(enemyPositions[i].position.x, enemyPositions[i].position.y) == attackSquare)
            {
                Debug.Log("ENEMY HITTTT!!!!");
                Instantiate(tempHitSprite, enemyPositions[i]);
                playerAttackPoints -= 1;
                break;
            }
            else 
            {
                Debug.Log("MISSSS" + enemyPositions[i].position);
            }
        }
    }

    public Vector2 NormalizInput(Vector2 input)
    {
        if(input.x > 0)
        {
            input.x = 1;
        }
        else if(input.x < 0)
        {
            input.x = -1;
        }

        if(input.y > 0)
        {
            input.y = 1;
        }
        else if (input.y < 0)
        {
            input.y = -1;
        }

            return input;
        
    }

    public Vector2 DiagonalBlock(Vector2 input)
    {
        if (input.x > 0 && input.y > 0 ||
            input.x < 0 && input.y < 0 ||
            input.x > 0 && input.y < 0 ||
            input.x < 0 && input.y > 0
            )
        {
            return Vector2.zero;
        }
        else 
        {
            return input;
        }
    }

    public void StartMovePhase()
    {
        UpdateEnemyPositions();
        ResetMovePoints();
        ResetAttackPoints();
        movePhase = true;
    }

    public void StopMovePhase()
    {
        //turnManager.SwitchTurn("Enemy", "Move");
        turnManager.SwitchTurn("Player", "Attack");
        movePhase = false;
    }

    void ResetMovePoints()
    {

        playerMovePoints = maxPlayerMovePoints;
        UpdatePlayerMovePointText();
    }

    void ResetAttackPoints()
    {
        playerAttackPoints = maxPlayerAttackPoints;
        UpdatePlayerAttackPointText();
    }

    void UpdatePlayerMovePointText()
    {
        movementPointsText.text = "Move Points: " + playerMovePoints;
    }
    void UpdatePlayerAttackPointText()
    {
        attackPointsText.text = "Attack Points: " + playerAttackPoints;
    }

    void PickRandomFootStep()
    {
        int random = Random.Range(0, footSteps.Length);
        audioSource.clip = footSteps[random];
        audioSource.Play();
    }

    void StartAttackPhase()
    {
        Debug.Log("Start Attack Phase, Player");
        attackPhase = true;
    }

    void MakeAttack()
    {

    }

    void StopAttackPhase()
    {
        Debug.Log("Stop Attack Phase, Player");
        attackPhase = false;
        turnManager.SwitchTurn("Enemy", "Attack");
    }
}
