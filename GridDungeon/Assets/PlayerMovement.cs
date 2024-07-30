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

    public Vector2 playerPosition;

    public bool isFreeUp, isFreeDown, isFreeRight, isFreeLeft;

    public NeighbourSquareCollector neighbourSquareCollector;

    public TMP_Text movementPointsText;
    public AudioClip[] footSteps;
    AudioSource audioSource;

    public TurnManager turnManager;
    public bool movePhase;

    private void OnEnable()
    {
        TurnManager.OnPlayerMove += StartMovePhase;
    }
    private void OnDisable()
    {
        TurnManager.OnPlayerMove -= StartMovePhase;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveAction = playerInput.actions.FindAction("Move");
        playerPosition = player.transform.position;
       
        audioSource = GetComponent<AudioSource>();

        UpdatePlayerMovePointText();

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
                    }
                    #endregion

                    #region Account for Diagonal Moves
                    if (moveValue.x > 0 && moveValue.y > 0 ||
                            moveValue.x < 0 && moveValue.y < 0 ||
                            moveValue.x > 0 && moveValue.y < 0 ||
                            moveValue.x < 0 && moveValue.y > 0)
                    {
                        moveValue = Vector2.zero;
                    }
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

            UpdatePlayerMovePointText();

        }
        else if (context.canceled) // If at the end of move, player is out of points, trigger end of Move Phase, go to Enemy Phase
        {

        }

       

       

    }

    public void StartMovePhase()
    {
        ResetMovePoints();
        movePhase = true;
    }

    public void StopMovePhase()
    {
        turnManager.SwitchTurn("Enemy", "Move");
        //turnManager.SwitchTurn("Player", "Attack");
        movePhase = false;
    }

    void ResetMovePoints()
    {

        playerMovePoints = maxPlayerMovePoints;
        UpdatePlayerMovePointText();
    }

    void UpdatePlayerMovePointText()
    {
        movementPointsText.text = "Move Points: " + playerMovePoints;
    }

    void PickRandomFootStep()
    {
        int random = Random.Range(0, footSteps.Length);
        audioSource.clip = footSteps[random];
        audioSource.Play();
    }
}
