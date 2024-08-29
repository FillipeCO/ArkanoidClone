using UnityEngine;

public class BrickFloatingAnimation : MonoBehaviour
{
    public float amplitude = 0.1f; // Altura do balanço
    public float speed = 2f; // Velocidade da animação

    private Vector3 initialPosition; // Posição inicial do objeto

    void Start()
    {
        // Armazena a posição inicial do objeto
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Calcula a nova posição usando uma função senoidal para o movimento suave de subida e descida
        float newY = Mathf.Sin(Time.time * speed) * amplitude;

        // Aplica a posição calculada ao objeto, mantendo a posição inicial x e z
        transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + newY, initialPosition.z);
    }
}
