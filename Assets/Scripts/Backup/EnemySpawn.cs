using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class EnemySpawn : NetworkBehaviour
{

    public GameObject prefabInimigo;


    public override void OnStartServer()
    {
        base.OnStartServer();

        Invoke("SpawnarInimigo", 2);
    }

    [Server] //apenas o server pode chamar
    void SpawnarInimigo()
    {
        GameObject inimigo = Instantiate(prefabInimigo, transform);
        base.Spawn(inimigo);
    }


}