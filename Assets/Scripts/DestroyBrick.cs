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

    // Novo sprite a ser exibido quando o brick for destru�do
    public Sprite destroyedSprite;

    // Refer�ncia ao SpriteRenderer do brick
    private SpriteRenderer spriteRenderer;

    // Refer�ncia ao Collider2D do brick
    private Collider2D brickCollider;

    // Dura��o da transi��o suave
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
        // Verifica se o objeto colidido � a bola
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Adiciona 10 pontos
            scoreController.AtualizarPontuacao(scoreController.points + 10);
            AtualizarTextoPontuacao(); // Atualiza o texto na tela

            // Desativa o collider para evitar mais colis�es
            brickCollider.enabled = false;

            // Notifica o GameController que o Brick foi destru�do
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                gameController.RemoverBrick(gameObject);
            }

            // Inicia a transi��o suave do sprite
            StartCoroutine(TransitionToDestroyedSprite());
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

    // Coroutine para realizar a transi��o suave do sprite
    private IEnumerator TransitionToDestroyedSprite()
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;

        // Enquanto a transi��o n�o estiver completa
        while (elapsedTime < transitionDuration)
        {
            // Substituir o sprite pelo novo sprite destru�do
            spriteRenderer.sprite = destroyedSprite;

            elapsedTime += Time.deltaTime;

            // Interpolar a cor do sprite atual para transparente
            spriteRenderer.color = Color.Lerp(initialColor, new Color(1, 1, 1, 0), elapsedTime / transitionDuration);

            yield return null;
        }
        Destroy(gameObject);
    }
}
