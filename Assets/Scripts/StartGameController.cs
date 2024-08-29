using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    void Update()
    {
        // Verifica se o jogador pressionou Espaço ou Y no joystick
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            IniciarJogo();
        }
    }

    void IniciarJogo()
    {
        // Carrega a cena "Level 1"
        SceneManager.LoadScene("Level 1");
    }
}
