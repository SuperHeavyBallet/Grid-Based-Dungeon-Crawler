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


    // Start is called before the first frame update
    void Start()
    {
        moveAction = playerInput.actions.FindAction("Move");
        playerPosition = player.transform.position;
       
        audioSource = GetComponent<AudioSource>();
        playerMovePoints = maxPlayerMovePoints;
        UpdatePlayerMovePointText();

    }

    // Update is called once per frame
    void Update()
    {
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


       
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        { }

        else if (context.performed)
        { Debug.Log("Action was performed");

           

            if (playerMovePoints >0)
            {
                Vector2 moveValue = moveAction.ReadValue<Vector2>();

               

                if (moveValue.x > 0)
                {
                    moveValue.x = 1;
                    playerSprite.transform.localScale = new Vector3(1,1,1);
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

                if (moveValue.x > 0 && moveValue.y > 0 ||
                        moveValue.x < 0 && moveValue.y < 0 ||
                        moveValue.x > 0 && moveValue.y < 0 ||
                        moveValue.x < 0 && moveValue.y > 0)
                {
                    Debug.Log("A CONFLICT!: " + moveValue);
                    moveValue = Vector2.zero;
                }

                Debug.Log("Move: " + moveValue);

                if (moveValue.x > 0 && isFreeRight)
                {
                    PickRandomFootStep();
                    this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                    playerMovePoints -= 1;

                }

                if (moveValue.x < 0 && isFreeLeft)
                {
                    PickRandomFootStep();
                    this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                    playerMovePoints -= 1;
                }
                if (moveValue.y > 0 && isFreeUp)
                {
                    PickRandomFootStep();
                    this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                    playerMovePoints -= 1;
                }
                if (moveValue.y < 0 && isFreeDown)
                {
                    PickRandomFootStep();
                    this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);
                    playerMovePoints -= 1;
                }


                UpdatePlayerMovePointText();
                
                if (playerMovePoints == 0)
                {
                    turnManager.SwitchTurn("Attack");
                    Invoke("ResetMovePoints", 2);
                }
            }
            
                
            

            

        }
        else if (context.canceled)
        {  }

       

       

    }

    void ResetMovePoints()
    {
        turnManager.SwitchTurn("Move");

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
