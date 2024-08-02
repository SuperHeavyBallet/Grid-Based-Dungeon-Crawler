using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public ZombieHealth zombieHealthScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
      
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("HITTTTT");
        }
    }
}
