using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator animator;

    // Vari�vel de ataque
    public bool podeAtacar = false;

    // Vari�veis controle de detec��o do inimigo
    public float velocidade;
    bool chegouAoDestino = true;

    bool rondarArea = true;
    bool seguirJogador = false;

    Vector3 destino;
    Vector3 areaOriginal;

    Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        destino = Vector3.zero;
        areaOriginal = transform.Find("AreaDeteccao").transform.localScale;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if( podeAtacar == true )
        {
            animator.SetBool("estaAtacando", true);
            return;
        }

        // Criar uma fun��o que desabilita o podeAtacar
        // e desabilita tamb�m a anima��o estaAtacando
        // Na anima��o do inimigo, criar um evento
        // que chama essa fun��o. Fim :)

        if( seguirJogador == true)
        {
            animator.SetBool("estaAndando", true);
            Vector3 posicaoJogador = GameObject.FindWithTag("Player").transform.position;
            transform.position = Vector3.MoveTowards(transform.position, posicaoJogador, Time.deltaTime * velocidade);
            
            // Olhar para o jogador
            float rotacaoX = transform.rotation.x;
            transform.LookAt( posicaoJogador );
            // Corrigir posi��o X
            transform.rotation = Quaternion.Euler(
                    rotacaoX,
                    transform.rotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z
                );
        }

        if( rondarArea == true)
        {
            if (chegouAoDestino == true)
            {
                float posicaoX = Random.Range(-areaOriginal.x / 2, areaOriginal.x / 2);
                float posicaoZ = Random.Range(-areaOriginal.z / 2, areaOriginal.z / 2);
                destino = new Vector3(posicaoX, transform.position.y, posicaoZ);

                Invoke("desabilitaChegouAoDestino", 2);
            }

            if (chegouAoDestino == false)
            {
                animator.SetBool("estaAndando", true);
                transform.position = Vector3.MoveTowards(transform.position, destino, Time.deltaTime * velocidade);
            }

            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                chegouAoDestino = true;
                animator.SetBool("estaAndando", false);
            }
        }
        
    }

    void desabilitaAtaque()
    {
        animator.SetBool("estaAtacando", false);
        podeAtacar = false;
    }

    void desabilitaChegouAoDestino()
    {
        chegouAoDestino = false;
        transform.LookAt( destino );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( rb.velocity.magnitude < 0.1f )
        {
            chegouAoDestino = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if( collider.gameObject.tag == "Player")
        {
            seguirJogador = true;
            rondarArea = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            seguirJogador = false;
            rondarArea = true;
        }
    }

}
