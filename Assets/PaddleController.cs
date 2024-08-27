using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Velocidade de movimento da raquete
    public float moveSpeed = 10f;

    // Limites para o movimento da raquete no eixo X
    public float minX = -7.5f;
    public float maxX = 7.5f;

    void Update()
    {
        // Captura a entrada do teclado e joystick
        float moveInput = Input.GetAxis("Horizontal");

        // Calcula a nova posição da raquete
        Vector3 newPosition = transform.position + Vector3.right * moveInput * moveSpeed * Time.deltaTime;

        // Limita o movimento da raquete dentro dos limites definidos
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Atualiza a posição da raquete
        transform.position = newPosition;
    }
}
