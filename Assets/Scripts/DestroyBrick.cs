using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyBrick : MonoBehaviour
{
    // Referência ao componente TextMeshPro
    private TextMeshProUGUI textoPontuacao;

    // Referência ao ScoreController
    private ScoreController scoreController;

    // Novo sprite a ser exibido quando o brick for destruído
    public Sprite destroyedSprite;

    // Referência ao SpriteRenderer do brick
    private SpriteRenderer spriteRenderer;

    // Referência ao Collider2D do brick
    private Collider2D brickCollider;

    // Duração da transição suave
    public float transitionDuration = 0.5f;

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

        // Encontrar o SpriteRenderer do brick e o collider 2D do brick
        spriteRenderer = GetComponent<SpriteRenderer>();
        brickCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the brick.");
        }

        if (brickCollider == null)
        {
            Debug.LogError("Collider2D not found on the brick.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido é a bola
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Adiciona 10 pontos
            scoreController.AtualizarPontuacao(scoreController.points + 10);
            AtualizarTextoPontuacao(); // Atualiza o texto na tela

            // Desativa o collider para evitar mais colisões
            brickCollider.enabled = false;

            // Notifica o GameController que o Brick foi destruído
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                gameController.RemoverBrick(gameObject);
            }

            // Inicia a transição suave do sprite
            StartCoroutine(TransitionToDestroyedSprite());
        }
    }

    // Método para atualizar o texto da pontuação na tela
    private void AtualizarTextoPontuacao()
    {
        if (scoreController != null)
        {
            textoPontuacao.text = "Score: " + scoreController.points.ToString();
        }
    }

    // Coroutine para realizar a transição suave do sprite
    private IEnumerator TransitionToDestroyedSprite()
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;

        // Enquanto a transição não estiver completa
        while (elapsedTime < transitionDuration)
        {
            // Substituir o sprite pelo novo sprite destruído
            spriteRenderer.sprite = destroyedSprite;

            elapsedTime += Time.deltaTime;

            // Interpolar a cor do sprite atual para transparente
            spriteRenderer.color = Color.Lerp(initialColor, new Color(1, 1, 1, 0), elapsedTime / transitionDuration);

            yield return null;
        }
        Destroy(gameObject);
    }
}
