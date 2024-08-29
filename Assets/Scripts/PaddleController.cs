using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float minX = -7.5f;
    public float maxX = 7.5f;

    void Update()
    {
        Vector3 newPosition = transform.position;

        // Captura a entrada do teclado e joystick
        float moveInput = Input.GetAxis("Horizontal");

        // Verifica se há movimento no mouse
        if (Input.GetAxis("Mouse X") != 0)
        {
            // Movimentação pelo mouse
            float mousePositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            newPosition.x = Mathf.Clamp(mousePositionX, minX, maxX);
        }
        else if (moveInput != 0)
        {
            // Movimentação pelo teclado ou joystick
            newPosition.x += moveInput * moveSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        }

        // Atualiza a posição da raquete
        transform.position = newPosition;
    }
}
