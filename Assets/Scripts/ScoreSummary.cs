using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ScoreSummary : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para mostrar a pontua��o final
    public TextMeshProUGUI multiplierText; // Texto para mostrar o motivo da multiplica��o
    public SceneAsset proximaCena;
    private int finalScore;

    void Start()
    {
        if (ScoreController.Instance != null)
        {
            // Acessa a pontua��o e a quantidade de bolas restantes atrav�s da inst�ncia Singleton
            int baseScore = ScoreController.Instance.points;
            int remainingBalls = ScoreController.Instance.remainingBalls;

            // Calcula a pontua��o final com base na quantidade de bolas restantes
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
                Debug.LogError("ScoreController n�o encontrado na cena.");
            }
        }
        else
        {
            Debug.LogError("ScoreController instance not found!");
        }
    }

    void Update()
    {
        // Avan�ar para a pr�xima cena ao apertar Espa�o ou Y no joystick
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            AvancarParaProximaCena();
        }
    }

    void AvancarParaProximaCena()
    {
        if (proximaCena != null)
        {
            // Obt�m o �ndice da cena com base na refer�ncia do SceneAsset
            string nomeCena = proximaCena.name;
            ScoreController.Instance.points = finalScore;
            SceneManager.LoadScene(nomeCena);
        }
        else
        {
            Debug.LogError("Proxima cena n�o definida!");
        }
    }
}
