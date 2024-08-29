using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyBrick : MonoBehaviour
{
    // Variável estática para armazenar a pontuação
    public static int pontuacao = 0;

    // Referência ao componente TextMeshPro
    private TextMeshProUGUI textoPontuacao;

    // Referência ao GameController
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
        // Verifica se o objeto colidido é a bola
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Adiciona 10 pontos
            pontuacao += 10;
            AtualizarTextoPontuacao(); // Atualiza o texto na tela

            // Notifica o GameController que o Brick foi destruído
            if (gameController != null)
            {
                gameController.RemoverBrick(gameObject);
            }

            // Destrói o Brick
            Destroy(gameObject);
        }
    }

    // Método para atualizar o texto da pontuação na tela
    private void AtualizarTextoPontuacao()
    {
        textoPontuacao.text = "Score: " + pontuacao.ToString();
    }
}
