using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyBrick : MonoBehaviour
{
    // Refer�ncia ao componente TextMeshPro
    private TextMeshProUGUI textoPontuacao;

    // Refer�ncia ao ScoreController
    private ScoreController scoreController;

    private void Start()
    {
        // Encontrar o elemento de texto no Canvas
        textoPontuacao = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();

        // Encontrar o ScoreController na cena
        scoreController = FindObjectOfType<ScoreController>();

        if (scoreController == null)
        {
            Debug.LogError("ScoreController not found in the scene.");
        }
        else
        {
            AtualizarTextoPontuacao();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido � a bola
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Adiciona 10 pontos
            scoreController.AtualizarPontuacao(scoreController.points + 10);
            AtualizarTextoPontuacao(); // Atualiza o texto na tela

            // Notifica o GameController que o Brick foi destru�do
            GameController gameController = FindObjectOfType<GameController>();
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
        if (scoreController != null)
        {
            textoPontuacao.text = "Score: " + scoreController.points.ToString();
        }
    }
}
