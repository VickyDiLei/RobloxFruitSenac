using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        if( collider.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<EnemyController>().podeAtacar = true;
        }
    }
}
