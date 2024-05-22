using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;

public class CameraController : NetworkBehaviour
{

    Transform jogador;

    public float sensibilidade = 5f;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();

        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }

        jogador = GameObject.FindWithTag("Player").transform;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = jogador.position - new Vector3(0, -1, 0);

        if( Input.GetKey( KeyCode.Mouse1 ) == false)
            return;        

        mouseX += Input.GetAxis("Mouse X") * sensibilidade;
        mouseY += Input.GetAxis("Mouse Y") * sensibilidade;

        mouseY = Mathf.Clamp( mouseY, -90f, 90f );

        transform.rotation = Quaternion.Euler( -mouseY, mouseX, 0 );

    }
}
