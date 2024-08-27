using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyBrick : MonoBehaviour
{
    // Vari�vel est�tica para armazenar a pontua��o
    public static int pontuacao = 0;

    // Refer�ncia ao componente TextMeshPro
    private TextMeshProUGUI textoPontuacao;

    // Refer�ncia ao GameController
    private GameController gameController;

    private void Start()
    {
        // Encontrar o elemento de texto no Canvas
        textoPontuacao = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        AtualizarTextoPontuacao();

        // Encontrar o GameController na cena
        gameController = FindObjectOfType<GameController>();

        if (gameController == null)
        {
            Debug.LogError("GameController not found in the scene.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido � a bola
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Adiciona 10 pontos
            pontuacao += 10;
            AtualizarTextoPontuacao(); // Atualiza o texto na tela

            // Notifica o GameController que o Brick foi destru�do
            if (gameController != null)
            {
                gameController.RemoverBrick(gameObject);
            }

            // Destr�i o Brick
            Destroy(gameObject);
        }
    }

    // M�todo para atualizar o texto da pontua��o na tela
    private void AtualizarTextoPontuacao()
    {
        textoPontuacao.text = "Score: " + pontuacao.ToString();
    }
}
