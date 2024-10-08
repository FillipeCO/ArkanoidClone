using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButtonController : MonoBehaviour
{
    public Button restartButton; // Bot�o RESTART
    public Button quitButton;    // Bot�o QUIT GAME
    private Button selectedButton; // Bot�o atualmente selecionado
    private ScoreController scoreController;// Refer�ncia ao ScoreController

    void Start()
    {
        // Definir o bot�o RESTART como o inicialmente selecionado
        selectedButton = restartButton;
        selectedButton.Select();
    }

    void Update()
    {
        // Navegar entre os bot�es usando os direcionais do joystick ou teclas A e D
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0)
        {
            SelecionarBotao(restartButton);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0)
        {
            SelecionarBotao(quitButton);
        }

        // Executar a a��o do bot�o selecionado ao apertar Espa�o, Y, ou clicar com o bot�o esquerdo do mouse
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            if (selectedButton == restartButton)
            {
                scoreController = FindObjectOfType<ScoreController>();
                scoreController.AtualizarPontuacao(scoreController.points = 0);
                ReiniciarJogo();
            }
            else if (selectedButton == quitButton)
            {
                SairDoJogo();
            }
        }
    }

    // Seleciona o bot�o e garante que apenas um esteja selecionado por vez
    void SelecionarBotao(Button botao)
    {
        if (selectedButton != null)
        {
            // Desseleciona o bot�o anterior
            ColorBlock cb = selectedButton.colors;
            cb.normalColor = cb.disabledColor; // Alterar o estado para o visual desabilitado
            selectedButton.colors = cb;
        }

        // Seleciona o novo bot�o
        selectedButton = botao;
        selectedButton.Select();

        // Atualiza o visual para o estado highlighted
        ColorBlock newCb = selectedButton.colors;
        newCb.normalColor = newCb.highlightedColor; // Alterar o estado para o visual highlighted
        selectedButton.colors = newCb;
    }

    // Chamado quando o mouse entra em um bot�o
    public void OnMouseEnter(Button botao)
    {
        SelecionarBotao(botao);
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SairDoJogo()
    {
        Application.Quit(); // Fecha o jogo
        Debug.Log("O jogo foi encerrado."); // Para depura��o no Editor
    }
}
