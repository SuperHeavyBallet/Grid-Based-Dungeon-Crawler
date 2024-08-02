using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieHealth : MonoBehaviour
{
    int maxHealthPoints = 2;
    public int healthPoints;

    public TMP_Text healthPointsText;



    // Start is called before the first frame update
    void Start()
    {

        ResetHealthPoints();
        UpdateHealthPointsText();

        Debug.Log("Hello World?");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   void ResetHealthPoints()
    {
        healthPoints = maxHealthPoints;
    }

    void UpdateHealthPointsText()
    {
        healthPointsText.text = healthPoints.ToString();
    }
}
