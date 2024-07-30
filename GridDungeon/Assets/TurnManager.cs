using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public string currentTurn, turnPhase;
    public TMP_Text currentTurnText, turnPhaseText;

    // Possible Turns "Enemy" or "Player"
    // Possible Phases "Move" or "Attack"

    public PlayerMovement playerMovement;
    public ZombieMovement zombieMovement;

    public delegate void PlayerMove();
    public static event PlayerMove OnPlayerMove;

    public delegate void EnemyMove();
    public static event EnemyMove OnEnemyMove;

    public delegate void EnemyAttack();
    public static event EnemyAttack OnEnemyAttack;

    public int completedEnemies = 0;
    public int enemiesInGame = 0;

    public string prevTurn, prevPhase;
    
    // Turn/Phase Order
    // Zombie Move
    // Player Move
    // Player Attack
    // Zombie Attack
    // Repeat
    private void OnEnable()
    {
        ZombieMovement.OnEnemyCompletedTurn += AddCompletedEnemy;
    }
    private void OnDisable()
    {
        ZombieMovement.OnEnemyCompletedTurn -= AddCompletedEnemy;
    }
    // Start is called before the first frame update
    void Start()
    {

        Invoke("FirstTurn", 4f);
        

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        enemiesInGame = enemies.Length;

    }

    void FirstTurn()
    {
        SwitchTurn("Enemy", "Move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTurn(string newTurn, string newPhase)
    {
        if (newTurn == "Enemy" && newPhase == "Move")
        {
            prevTurn = "Enemy";
            prevPhase = "Attack";
        }
        else if (newTurn == "Player" && newPhase == "Move" )
        {
            prevTurn = "Enemy";
            prevPhase = "Move";
        }
        else if (newTurn == "Player" && newPhase == "Attack")
        {
            prevTurn = "Player";
            prevPhase = "Move";
        }
        else if (newTurn == "Enemy" && newPhase == "Attack")
        {
            prevTurn = "Player";
            prevPhase = "Attack";
        }

        Debug.Log("New: " + newTurn + " " + newPhase + ", Prev: " + prevTurn + " " + prevPhase);

        turnPhase = newTurn;
        turnPhase = newPhase;

        turnPhaseText.text = "Phase: " + newTurn;
        currentTurnText.text = "Turn: " + newPhase;

        if (newTurn == "Player" && newPhase == "Move")
        {
            if (OnPlayerMove != null)
            {
                OnPlayerMove();
            }

        }
        else if (newTurn == "Enemy" && newPhase == "Move")
        {
            if (OnEnemyMove != null)
            {
                completedEnemies = 0;
                OnEnemyMove();
            }
            
        }
    }

    public void AddCompletedEnemy()
    {
        completedEnemies += 1;

        if (completedEnemies == enemiesInGame)
        {
           // Debug.Log("All Enemies Completed");

            //Go To Player Turn
        }
    }

 
}
