using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public PlayerInput playerInput;
    InputAction moveAction;

    public Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        moveAction = playerInput.actions.FindAction("Move");
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue != new Vector2(0, 0))
        {
            Move(moveValue);
        }
        

    }

    public void Move(Vector2 moveValue)
    {
        Debug.Log("Move: " + moveValue);
        this.transform.position = new Vector2(this.transform.position.x + moveValue.x, this.transform.position.y + moveValue.y);

  
    }
}
