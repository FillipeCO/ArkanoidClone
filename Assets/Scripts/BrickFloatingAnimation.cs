using UnityEngine;

public class BrickFloatingAnimation : MonoBehaviour
{
    public float amplitude = 0.1f; // Altura do balan�o
    public float speed = 2f; // Velocidade da anima��o

    private Vector3 initialPosition; // Posi��o inicial do objeto

    void Start()
    {
        // Armazena a posi��o inicial do objeto
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Calcula a nova posi��o usando uma fun��o senoidal para o movimento suave de subida e descida
        float newY = Mathf.Sin(Time.time * speed) * amplitude;

        // Aplica a posi��o calculada ao objeto, mantendo a posi��o inicial x e z
        transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + newY, initialPosition.z);
    }
}
