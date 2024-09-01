using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class ScoreSummary : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para mostrar a pontua��o final
    public TextMeshProUGUI multiplierText; // Texto para mostrar o motivo da multiplica��o
    public AudioClip avancarSom;
    private AudioSource audioSource;
    public string proximaCena; // Nome da pr�xima cena
    private int finalScore;
    public float delayAntesDeAvancar = 1.0f; // Tempo de atraso antes de avan�ar para a pr�xima cena

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (ScoreController.Instance != null)
        {
            // Acessa a pontua��o e a quantidade de bolas restantes atrav�s da inst�ncia Singleton
            int baseScore = ScoreController.Instance.points;
            int remainingBalls = ScoreController.Instance.remainingBalls;

            // Calcula a pontua��o final com base na quantidade de bolas restantes
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
            StartCoroutine(AvancarParaProximaCena());
        }
    }

    IEnumerator AvancarParaProximaCena()
    {
        if (proximaCena != null)
        {
            if (proximaCena != "Tela Inicial")
            {
                PlaySound(avancarSom); // Toca o som de avancar para a pr�xima tela

                // Obt�m o �ndice da cena com base na refer�ncia do SceneAsset
                string nomeCena = proximaCena;
                ScoreController.Instance.points = finalScore;

                // Aguarda o tempo especificado antes de avan�ar
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
            Debug.LogError("Proxima cena n�o definida!");
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
