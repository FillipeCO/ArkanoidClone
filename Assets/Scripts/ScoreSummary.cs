using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ScoreSummary : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para mostrar a pontuação final
    public TextMeshProUGUI multiplierText; // Texto para mostrar o motivo da multiplicação
    public SceneAsset proximaCena;
    private int finalScore;

    void Start()
    {
        if (ScoreController.Instance != null)
        {
            // Acessa a pontuação e a quantidade de bolas restantes através da instância Singleton
            int baseScore = ScoreController.Instance.points;
            int remainingBalls = ScoreController.Instance.remainingBalls;

            // Calcula a pontuação final com base na quantidade de bolas restantes
            finalScore = baseScore * remainingBalls;

            // Atualiza os textos na tela
            scoreText.text = "Final Score: " + baseScore.ToString() + " X " + remainingBalls.ToString() + " Balls " + " = " + finalScore.ToString();
            multiplierText.text = "Score X " + remainingBalls + " due to remaining balls.";

            ScoreController scoreController = FindObjectOfType<ScoreController>();
            if (scoreController != null)
            {
                scoreController.AtualizarPontuacao(finalScore);
            }
            else
            {
                Debug.LogError("ScoreController não encontrado na cena.");
            }
        }
        else
        {
            Debug.LogError("ScoreController instance not found!");
        }
    }

    void Update()
    {
        // Avançar para a próxima cena ao apertar Espaço ou Y no joystick
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            AvancarParaProximaCena();
        }
    }

    void AvancarParaProximaCena()
    {
        if (proximaCena != null)
        {
            // Obtém o índice da cena com base na referência do SceneAsset
            string nomeCena = proximaCena.name;
            ScoreController.Instance.points = finalScore;
            SceneManager.LoadScene(nomeCena);
        }
        else
        {
            Debug.LogError("Proxima cena não definida!");
        }
    }
}
