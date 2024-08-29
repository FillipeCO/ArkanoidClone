using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar a pr�xima cena

public class GameController : MonoBehaviour
{
    public List<GameObject> bricks; // Lista para armazenar todos os objetos Brick
    public SceneAsset proximaCena; // Refer�ncia visual para a pr�xima cena

    private void Start()
    {
        // Inicializar a lista e encontrar todos os objetos com a tag "Brick" na cena
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
    }

    private void Update()
    {
        // Verifica se todos os Bricks foram destru�dos
        if (bricks.Count == 0)
        {
            AvancarParaProximoNivel();
        }
    }

    // M�todo para ser chamado quando um Brick for destru�do
    public void RemoverBrick(GameObject brick)
    {
        bricks.Remove(brick);

        // Verifica se a lista est� vazia ap�s remover um Brick
        if (bricks.Count == 0)
        {
            AvancarParaProximoNivel();
        }
    }

    // Avan�ar para o pr�ximo n�vel
    void AvancarParaProximoNivel()
    {
        if (proximaCena != null)
        {
            // Obt�m o �ndice da cena com base na refer�ncia do SceneAsset
            string nomeCena = proximaCena.name;
            SceneManager.LoadScene(nomeCena);
        }
        else
        {
            Debug.LogError("Proxima cena n�o definida!");
        }
    }
}
