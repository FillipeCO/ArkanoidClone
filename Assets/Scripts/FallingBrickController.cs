using UnityEngine;

public class FallingBrickController : MonoBehaviour
{
    public float velocidadeQueda = 0.5f; // Velocidade da queda
    public float distanciaQueda = 30f; // Distância total que o objeto irá cair
    public float tempoDeEspera = 0f; // Tempo de espera antes de começar a cair

    private Vector3 posicaoInicial;
    private Vector3 posicaoFinal;
    private bool comecarQueda = false;

    private void Start()
    {
        // Verifica se o objeto tem a tag "FallingBrick"
        if (gameObject.CompareTag("FallingBrick"))
        {
            // Define a posição inicial e final do objeto
            posicaoInicial = transform.position;
            posicaoFinal = new Vector3(transform.position.x, transform.position.y - distanciaQueda, transform.position.z);

            // Inicia a queda após o tempo de espera
            Invoke("IniciarQueda", tempoDeEspera);
        }
        else
        {
            Debug.LogWarning("O objeto não possui a tag 'FallingBrick'. O script FallingBrickController não será executado.");
        }
    }

    private void Update()
    {
        if (comecarQueda)
        {
            // Move o objeto suavemente para a posição final
            transform.position = Vector3.Lerp(transform.position, posicaoFinal, Time.deltaTime * velocidadeQueda);

            // Verifica se o objeto alcançou a posição final
            if (Vector3.Distance(transform.position, posicaoFinal) < 0.01f)
            {
                comecarQueda = false; // Para a queda
            }
        }
    }

    private void IniciarQueda()
    {
        comecarQueda = true;
    }
}
