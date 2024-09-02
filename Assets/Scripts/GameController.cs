using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<GameObject> bricks;
    public string proximaCena; // Nome da próxima cena
    public int remainingBalls;
    public static int points;
    private ScoreController scoreController;
    public GameObject pauseMenuUI;
    public Button restartButton; // Botão de reiniciar
    public Button quitButton;    // Botão de sair do jogo
    private Button selectedButton; // Botão atualmente selecionado
    private bool isPaused = false;

    private void Start()
    {
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        scoreController = FindObjectOfType<ScoreController>();
        pauseMenuUI.SetActive(false); // Esconde o menu de pausa inicialmente
        
    }

    private void Update()
    {
        if (bricks.Count == 0)
        {
            points = scoreController.points;
            remainingBalls = FindObjectOfType<BallsCounter>().quantidadeBolas;

            // Salva os dados no ScoreController
            ScoreController.Instance.SaveScoreData(remainingBalls, points);

            AvancarParaProximoNivel();
        }

        // Verifica se a tecla Esc ou Start no joystick foi pressionada para pausar o jogo
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Navegação no menu de pausa
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0)
            {
                SelecionarBotao(restartButton);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0)
            {
                SelecionarBotao(quitButton);
            }

            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
            {
                if (selectedButton == restartButton)
                {
                    ReiniciarJogo();
                }
                else if (selectedButton == quitButton)
                {
                    SairDoJogo();
                }
            }
        }
    }

    public void RemoverBrick(GameObject brick)
    {
        bricks.Remove(brick);

        // Verifica se a lista está vazia após remover um Brick
        if (bricks.Count == 0)
        {
            AvancarParaProximoNivel();
        }
    }

    // Pausa o jogo
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Para o tempo do jogo
        isPaused = true;
        //SelecionarBotao(restartButton); // Seleciona o botão de reiniciar por padrão
    }

    // Retoma o jogo
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Retoma o tempo do jogo
        isPaused = false;
    }

    // Seleciona o botão e garante que apenas um esteja selecionado por vez
    void SelecionarBotao(Button botao)
    {
        if (selectedButton != null)
        {
            var colors = selectedButton.colors;
            colors.normalColor = colors.disabledColor; // Muda a cor do botão anterior para a cor normal
            selectedButton.colors = colors;
        }

        selectedButton = botao;
        selectedButton.Select();

        var selectedColors = selectedButton.colors;
        selectedColors.normalColor = selectedColors.selectedColor; // Muda a cor do botão selecionado para a cor highlighted
        selectedButton.colors = selectedColors;
    }

    // Avançar para o próximo nível
    void AvancarParaProximoNivel()
    {
        if (proximaCena != null)
        {
            // Obtém o índice da cena com base na referência do SceneAsset
            string nomeCena = proximaCena;
            SceneManager.LoadScene(nomeCena);
        }
        else
        {
            Debug.LogError("Proxima cena não definida!");
        }
    }

    // Reiniciar o jogo
    public void ReiniciarJogo()
    {
        Time.timeScale = 1f; // Retoma o tempo antes de reiniciar o jogo
        scoreController = FindObjectOfType<ScoreController>();
        scoreController.AtualizarPontuacao(scoreController.points = 0);
        SceneManager.LoadScene("Level 1");
        Debug.Log("Reiniciou");
    }

    // Sair do jogo
    public void SairDoJogo()
    {
        Application.Quit();
    }
}
