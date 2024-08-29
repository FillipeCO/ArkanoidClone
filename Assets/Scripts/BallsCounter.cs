using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Para carregar a tela de derrota

public class BallsCounter : MonoBehaviour
{
    public int quantidadeBolas = 3; // Número inicial de bolas
    private TextMeshProUGUI textoBolas;

    private void Start()
    {
        textoBolas = GameObject.Find("Balls").GetComponent<TextMeshProUGUI>();
        AtualizarTextoBolas();
    }

    public void DecrementarBolas()
    {
        if (quantidadeBolas > 0)
        {
            quantidadeBolas--;
            AtualizarTextoBolas();

 
        }
        else
        {
            Debug.Log("Sem bolas restantes!");
            AvancarParaTelaDeDerrota();
        }
    }

    private void AtualizarTextoBolas()
    {
        textoBolas.text = "Balls:" + quantidadeBolas.ToString();
    }

    private void AvancarParaTelaDeDerrota()
    {
        // Carrega a tela de derrota
        SceneManager.LoadScene("TelaDeDerrota");
    }
}
