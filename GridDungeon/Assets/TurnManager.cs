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

    public delegate void ZombieMove();
    public static event ZombieMove OnZombieMove;

    public delegate void ZombieAttack();
    public static event ZombieAttack OnZombieAttack;

    public int completedZombies = 0;
    public int enemiesInGame = 0;
    

    private void OnEnable()
    {
        ZombieMovement.OnZombieCompletedTurn += AddCompletedZombie;
    }
    private void OnDisable()
    {
        ZombieMovement.OnZombieCompletedTurn -= AddCompletedZombie;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTurn = "Player";
        currentTurnText.text = "Turn: " + currentTurn;

        turnPhase = "Move";
        turnPhaseText.text = "Phase: " + turnPhase;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        enemiesInGame = enemies.Length;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTurn(string newTurn, string newPhase)
    {
        Debug.Log("Enter Switch Turn" + (newTurn + newPhase));
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
            if (OnZombieMove != null)
            {
                completedZombies = 0;
                OnZombieMove();
            }
            
        }
    }

    public void AddCompletedZombie()
    {
        completedZombies += 1;

        if (completedZombies == enemiesInGame)
        {
            Debug.Log("All Enemies Completed");

            //Go To Player Turn
        }
    }

 
}
