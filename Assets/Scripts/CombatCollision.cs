using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {

        if ( collider.GetComponent<CharacterStatus>() != null )
        {
            collider.GetComponent<CharacterStatus>().receberDano(1);
        }

    }
}
