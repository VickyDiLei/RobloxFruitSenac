using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;

public class CharacterStatus : NetworkBehaviour
{

    public int vidaTotal;
    public int vidaAtual;

    RectTransform barraVida; // barra de vida atual
    RectTransform barraTotal; // barra de vida total
    Transform canvas;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }

        vidaAtual = vidaTotal;
        barraVida = transform.Find("BarraVida").transform.Find("VidaAtual").GetComponent<RectTransform>();
        barraTotal = transform.Find("BarraVida").transform.Find("VidaTotal").GetComponent<RectTransform>();
        canvas = transform.Find("BarraVida");
    }

    private void Update()
    {
        if(base.IsOwner == false) // isOwner para se referir ao dono do script, ao proprietário
        {
            return; // se não for o dono do script, o código encerra aqui.
        }
        // Fazer o canvas olhar para a c�mera
        //canvas.LookAt(Camera.main.transform);
        transform.Find("Camera").gameObject.SetActive(true);
    }

    void atualizarHUD()
    {
        // Calcular quanto deve ser reduzido de vida
        float reducao = (float)vidaAtual / (float)vidaTotal;
        reducao = reducao * 100;
        // Diminuir a barra de vida
        barraVida.sizeDelta = new Vector2(reducao, barraVida.sizeDelta.y );
        // Realocar a posi��o X da barra de vida para a esquerda
        float posicao = (barraTotal.sizeDelta.x - barraVida.sizeDelta.x) / 2;
        barraVida.anchoredPosition = new Vector2( posicao , barraVida.anchoredPosition.y );
    }

    public void receberDano(int valor)
    {
        vidaAtual -= valor;
        verificaMorte();
        atualizarHUD();
    }

    void verificaMorte()
    {
        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }

}
