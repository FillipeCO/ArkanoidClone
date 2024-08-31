using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements; // Para carregar a pr�xima cena

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
