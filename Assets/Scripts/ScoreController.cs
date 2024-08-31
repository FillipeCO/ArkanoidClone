using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;

    public int remainingBalls;
    public int points;
    public void AtualizarPontuacao(int novaPontuacao)
    {
        points = novaPontuacao;
    }

    private void Awake()
    {
        // Verifica se já existe uma instância do ScoreController
        if (Instance == null)
        {
            // Se não existir, define esta como a instância
            Instance = this;
            // Garante que o ScoreController não seja destruído ao mudar de cena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existir uma instância, destrói esta para manter o Singleton
            Destroy(gameObject);
        }
    }

    // Método para salvar os dados de remainingBalls e points
    public void SaveScoreData(int balls, int score)
    {
        remainingBalls = balls;
        points = score;
    }

    // Métodos para acessar os dados de remainingBalls e points
    public int GetRemainingBalls()
    {
        return remainingBalls;
    }

    public int GetPoints()
    {
        return points;
    }
}
