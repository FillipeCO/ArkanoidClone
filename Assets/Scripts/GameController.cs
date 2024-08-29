using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar a próxima cena

public class GameController : MonoBehaviour
{
    public List<GameObject> bricks; // Lista para armazenar todos os objetos Brick
    public SceneAsset proximaCena; // Referência visual para a próxima cena

    private void Start()
    {
        // Inicializar a lista e encontrar todos os objetos com a tag "Brick" na cena
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
    }

    private void Update()
    {
        // Verifica se todos os Bricks foram destruídos
        if (bricks.Count == 0)
        {
            AvancarParaProximoNivel();
        }
    }

    // Método para ser chamado quando um Brick for destruído
    public void RemoverBrick(GameObject brick)
    {
        bricks.Remove(brick);

        // Verifica se a lista está vazia após remover um Brick
        if (bricks.Count == 0)
        {
            AvancarParaProximoNivel();
        }
    }

    // Avançar para o próximo nível
    void AvancarParaProximoNivel()
    {
        if (proximaCena != null)
        {
            // Obtém o índice da cena com base na referência do SceneAsset
            string nomeCena = proximaCena.name;
            SceneManager.LoadScene(nomeCena);
        }
        else
        {
            Debug.LogError("Proxima cena não definida!");
        }
    }
}
