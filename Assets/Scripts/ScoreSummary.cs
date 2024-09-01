using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class ScoreSummary : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para mostrar a pontuação final
    public TextMeshProUGUI multiplierText; // Texto para mostrar o motivo da multiplicação
    public AudioClip avancarSom;
    private AudioSource audioSource;
    public string proximaCena; // Nome da próxima cena
    private int finalScore;
    public float delayAntesDeAvancar = 1.0f; // Tempo de atraso antes de avançar para a próxima cena

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (ScoreController.Instance != null)
        {
            // Acessa a pontuação e a quantidade de bolas restantes através da instância Singleton
            int baseScore = ScoreController.Instance.points;
            int remainingBalls = ScoreController.Instance.remainingBalls;

            // Calcula a pontuação final com base na quantidade de bolas restantes
            finalScore = baseScore * remainingBalls;

            // Atualiza os textos na tela
            scoreText.text = "Score:" + "\n" + baseScore.ToString() + "\n" + "\n" +
                 remainingBalls.ToString() + "X" + "\n" + "\n" +
                 "Final Score:" + "\n" + finalScore.ToString();

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
            StartCoroutine(AvancarParaProximaCena());
        }
    }

    IEnumerator AvancarParaProximaCena()
    {
        if (proximaCena != null)
        {
            if (proximaCena != "Tela Inicial")
            {
                PlaySound(avancarSom); // Toca o som de avancar para a próxima tela

                // Obtém o índice da cena com base na referência do SceneAsset
                string nomeCena = proximaCena;
                ScoreController.Instance.points = finalScore;

                // Aguarda o tempo especificado antes de avançar
                yield return new WaitForSeconds(delayAntesDeAvancar);
                SceneManager.LoadScene(nomeCena);
            }
            if (proximaCena == "Tela Inicial")
            {
                ScoreController.Instance.points = 0;
                SceneManager.LoadScene("Tela Inicial");
            }


        }
        else
        {
            Debug.LogError("Proxima cena não definida!");
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
