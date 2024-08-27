using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverButtonController : MonoBehaviour
{
    public Button restartButton; // Bot�o RESTART
    public Button quitButton;    // Bot�o QUIT GAME
    private Button selectedButton; // Bot�o atualmente selecionado

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

        // Executar a a��o do bot�o selecionado ao apertar Espa�o ou Y
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
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

    void SelecionarBotao(Button botao)
    {
        selectedButton = botao;
        selectedButton.Select();
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
