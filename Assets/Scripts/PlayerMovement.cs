using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;

public class PlayerMovement : NetworkBehaviour
{

    public float velocidade = 10f;
    public static float gravidade = 9.87f;

    private float rotacaoX = 0f; //MUDANÇA

    public float sensibilidadeMouse = 300f; //MUDANÇA

    public static CharacterController cc;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }

        cc = GetComponent<CharacterController>();
        transform.Find("Camera").gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked; //MUDANÇA
    }


    void Update()
    {

        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }

        float direcao_x = InputController.inputHorizontal * velocidade * Time.deltaTime;
        float direcao_z = InputController.inputVertical * velocidade * Time.deltaTime;
        float direcao_y = -gravidade * Time.deltaTime;

        if ( PlayerJump.estadoPulo == EstadoPulo.Pulando)
        {

            direcao_y = Mathf.SmoothStep( gravidade, gravidade * 0.30f, PlayerJump.tempoDecorridoPulo / PlayerJump.tempoPulo);
            direcao_y = direcao_y * Time.deltaTime;

        }

        if ( PlayerJump.estadoPulo == EstadoPulo.Caindo)
        {
            direcao_y = Mathf.SmoothStep(-gravidade * 0.20f, -gravidade, PlayerJump.tempoDecorridoPulo / PlayerJump.tempoPulo);
            direcao_y = direcao_y * Time.deltaTime;
        }
        //MUDANÇA
        if (Input.GetMouseButton(1)) // Verifica se o botão direito do mouse está pressionado
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
            rotacaoX += mouseX;
            Quaternion rotacao = Quaternion.Euler(0f, rotacaoX, 0f);
            transform.rotation = rotacao;
        }//FIM DA MUDANÇA

        // Rota��o do personagem
        Vector3 frente = transform.TransformDirection(Vector3.forward);//Camera.main.transform.forward;
        Vector3 direita = transform.TransformDirection(Vector3.right);//Camera.main.transform.right;

        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        frente = frente * direcao_z;
        direita = direita * direcao_x;

        /*if( direcao_x != 0 || direcao_z != 0)
        {
            float angulo = Mathf.Atan2( frente.x + direita.x, frente.z + direita.z ) * Mathf.Rad2Deg;
            Quaternion rotacao = Quaternion.Euler(0, angulo, 0);
            //transform.rotation = rotacao;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, 0.15f);
        }*/

        Vector3 direcao_vertical = Vector3.up * direcao_y;
        Vector3 direcao_horizontal = frente + direita;

        Vector3 movimento = direcao_vertical + direcao_horizontal;
        cc.Move(movimento);
        

    }
}
