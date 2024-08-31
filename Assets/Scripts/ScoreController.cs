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
        // Verifica se j� existe uma inst�ncia do ScoreController
        if (Instance == null)
        {
            // Se n�o existir, define esta como a inst�ncia
            Instance = this;
            // Garante que o ScoreController n�o seja destru�do ao mudar de cena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se j� existir uma inst�ncia, destr�i esta para manter o Singleton
            Destroy(gameObject);
        }
    }

    // M�todo para salvar os dados de remainingBalls e points
    public void SaveScoreData(int balls, int score)
    {
        remainingBalls = balls;
        points = score;
    }

    // M�todos para acessar os dados de remainingBalls e points
    public int GetRemainingBalls()
    {
        return remainingBalls;
    }

    public int GetPoints()
    {
        return points;
    }
}
