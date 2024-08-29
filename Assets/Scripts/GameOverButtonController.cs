using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButtonController : MonoBehaviour
{
    public Button restartButton; // Botão RESTART
    public Button quitButton;    // Botão QUIT GAME
    private Button selectedButton; // Botão atualmente selecionado

    void Start()
    {
        // Definir o botão RESTART como o inicialmente selecionado
        selectedButton = restartButton;
        selectedButton.Select();
    }

    void Update()
    {
        // Navegar entre os botões usando os direcionais do joystick ou teclas A e D
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0)
        {
            SelecionarBotao(restartButton);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0)
        {
            SelecionarBotao(quitButton);
        }

        // Executar a ação do botão selecionado ao apertar Espaço, Y, ou clicar com o botão esquerdo do mouse
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
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

    // Seleciona o botão e garante que apenas um esteja selecionado por vez
    void SelecionarBotao(Button botao)
    {
        if (selectedButton != null)
        {
            selectedButton.OnDeselect(null); // Deseleciona o botão anterior
        }

        selectedButton = botao;
        selectedButton.Select();
    }

    // Chamado quando o mouse entra em um botão
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
        Debug.Log("O jogo foi encerrado."); // Para depuração no Editor
    }
}
