using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public string currentTurn, turnPhase;
    public TMP_Text currentTurnText, turnPhaseText;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = "Player";
        currentTurnText.text = "Turn: " + currentTurn;

        turnPhase = "Move";
        turnPhaseText.text = "Phase: " + turnPhase;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTurn(string newTurn)
    {
        turnPhase = newTurn;
        turnPhaseText.text = "Phase: " + turnPhase;
    }

 
}
