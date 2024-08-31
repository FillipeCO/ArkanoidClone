using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements; // Para carregar a próxima cena

public class GameController : MonoBehaviour
{
    public List<GameObject> bricks;
    public SceneAsset proximaCena;
    public int remainingBalls;
    public static int points;
    private ScoreController scoreController;

    private void Start()
    {
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        scoreController = FindObjectOfType<ScoreController>();
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
