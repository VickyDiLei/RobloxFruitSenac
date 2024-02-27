using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        //if( collider.CompareTag("Inimigo"))
        if( collider.GetComponent<EnemyController>() != null )
        {
            collider.GetComponent<EnemyController>().receberDano(1);
        }
    }
}
