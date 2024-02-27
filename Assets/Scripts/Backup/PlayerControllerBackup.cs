using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerBackup : MonoBehaviour
{

    public float gravidade = 9.87f;
    public float velocidade = 10f;
    CharacterController cc;

    public float tempoPulo = 2f;
    float tempoDecorridoPulo = 0;

    EstadoPulo estadoPulo;

    // Módulo de inputs
    float inputHorizontal;
    float inputVertical;
    bool inputPulo;

    Vector3 movimento;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        estadoPulo = EstadoPulo.Solo;
        movimento = Vector3.zero;
    }

    void Update()
    {

        // Módulo de identificação dos inputs
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
