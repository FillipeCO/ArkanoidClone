using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public float velocidadeInicial = 10f; // Velocidade da bola
    public float forcaRebate = 1.1f; // Multiplicador para aumentar a força do ricochete
    private Vector2 direcao;
    private bool movimentoIniciado = false;
    private Vector3 posicaoInicial = new Vector3(0f, -4.58f, 0f); // Posição inicial específica
    private BallsCounter ballsCounter; // Referência para o contador de bolas
    public float anguloRebateExtra = 15f; // Ângulo extra para aumentar a angulação do rebote

    // Adicione referências para os efeitos sonoros
    public AudioClip paddleHitSound;
    public AudioClip brickHitSound;
    public AudioClip endwallkHitSound;
    private AudioSource audioSource;

    private void Start()
    {
        direcao = Vector2.up;

        // Encontrar o contador de bolas na cena
        ballsCounter = FindObjectOfType<BallsCounter>();

        if (ballsCounter == null)
        {
            Debug.LogError("BallsCounter não encontrado na cena.");
        }

        // Adicionar o componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (!movimentoIniciado && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
        {
            IniciarMovimento();
            movimentoIniciado = true;
        }

        if (movimentoIniciado)
        {
            transform.Translate(direcao * velocidadeInicial * Time.deltaTime);
        }
    }

    void IniciarMovimento()
    {
        float anguloInicial = Random.Range(-45f, 45f);
        direcao = Quaternion.Euler(0f, 0f, anguloInicial) * Vector2.up;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            RebaterNaSuperficieAcumulandoForca(collision.GetContact(0).normal);
            // Tocar som de colisão com Brick
            PlaySound(brickHitSound);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            RebaterNaSuperficie(collision.GetContact(0).normal);
            // Tocar som de colisão com Paddle
            PlaySound(paddleHitSound);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            RebaterNaSuperficie(collision.GetContact(0).normal);
        }
        else if (collision.gameObject.CompareTag("EndWall"))
        {
            ReiniciarBola();
            // Verifica se o contador de bolas está disponível e decrementa
            if (ballsCounter != null)
            {
                ballsCounter.DecrementarBolas();
            }
            PlaySound(endwallkHitSound);
        }
    }

    void RebaterNaSuperficieAcumulandoForca(Vector2 normal)
    {
        direcao = Vector2.Reflect(direcao, normal).normalized;

        // Aplica um ângulo extra ao rebote para aumentar a angulação
        direcao = Quaternion.Euler(0f, 0f, Random.Range(-anguloRebateExtra, anguloRebateExtra)) * direcao;

        velocidadeInicial += forcaRebate; // Adiciona força de rebate
    }

    void RebaterNaSuperficie(Vector2 normal)
    {
        direcao = Vector2.Reflect(direcao, normal).normalized;

        // Aplica um ângulo extra ao rebote para aumentar a angulação
        direcao = Quaternion.Euler(0f, 0f, Random.Range(-anguloRebateExtra, anguloRebateExtra)) * direcao;
    }

    void ReiniciarBola()
    {
        // Resetar a posição e direção da bola
        transform.position = posicaoInicial;
        transform.rotation = Quaternion.identity;
        direcao = Vector2.up;
        velocidadeInicial = 10f; // Redefine a velocidade para a inicial
        movimentoIniciado = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
