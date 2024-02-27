using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private int vida = 0;

    void verificarMorte()
    {
        if (vida <= 0) {
            Destroy(gameObject);
        }
    }

    public void receberDano( int valor )
    {
        vida -= valor;
        verificarMorte();
    }

}
