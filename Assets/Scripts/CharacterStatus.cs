using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{

    public int vidaTotal;
    public int vidaAtual;

    RectTransform barraVida; // barra de vida atual
    RectTransform barraTotal; // barra de vida total
    Transform canvas;

    void Start()
    {
        vidaAtual = vidaTotal;
        barraVida = transform.Find("BarraVida").transform.Find("VidaAtual").GetComponent<RectTransform>();
        barraTotal = transform.Find("BarraVida").transform.Find("VidaTotal").GetComponent<RectTransform>();
        canvas = transform.Find("BarraVida");
    }

    private void Update()
    {
        // Fazer o canvas olhar para a câmera
        canvas.LookAt(Camera.main.transform);
    }

    void atualizarHUD()
    {
        // Calcular quanto deve ser reduzido de vida
        float reducao = (float)vidaAtual / (float)vidaTotal;
        reducao = reducao * 100;
        // Diminuir a barra de vida
        barraVida.sizeDelta = new Vector2(reducao, barraVida.sizeDelta.y );
        // Realocar a posição X da barra de vida para a esquerda
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
