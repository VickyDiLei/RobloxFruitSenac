using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Managing;


public class PlayerControllerBackup : NetworkBehaviour
{

    public float gravidade = 9.87f;
    public float velocidade = 10f;
    CharacterController cc;

    public float tempoPulo = 2f;
    float tempoDecorridoPulo = 0;

    EstadoPulo estadoPulo;

    // M�dulo de inputs
    float inputHorizontal;
    float inputVertical;
    bool inputPulo;

    Vector3 movimento;

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameObject.Find("Camera").gameObject.SetActive(false);

        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }
        
        cc = GetComponent<CharacterController>();

        transform.Find("Main Camera").gameObject.SetActive(true);
        
        estadoPulo = EstadoPulo.Solo;
        movimento = Vector3.zero;
    }

    void Update()
    {

        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }

        // M�dulo de identifica��o dos inputs
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputPulo = Input.GetKeyDown(KeyCode.Space);
        // ------------------------------------

        movimento.x = inputHorizontal * velocidade;
        movimento.z = inputVertical * velocidade;

        if( inputPulo && estadoPulo == EstadoPulo.Solo )
        {
            estadoPulo = EstadoPulo.Pulando;
            movimento.y = tempoPulo;
        }

        movimento.y -= gravidade * Time.deltaTime;
        
        cc.Move( movimento * Time.deltaTime );

    }
}
