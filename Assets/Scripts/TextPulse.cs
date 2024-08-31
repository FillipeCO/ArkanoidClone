using UnityEngine;
using TMPro;

public class TextPulse : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float minScale = 1.0f; // Tamanho m�nimo do texto
    public float maxScale = 1.5f; // Tamanho m�ximo do texto
    public float speed = 2.0f; // Velocidade da anima��o

    private bool isIncreasing = true;
    private float currentScale;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
        currentScale = minScale;
    }

    void Update()
    {
        // Anima��o de aumento e diminui��o suave do texto
        if (isIncreasing)
        {
            currentScale += Time.deltaTime * speed;
            if (currentScale >= maxScale)
            {
                currentScale = maxScale;
                isIncreasing = false;
            }
        }
        else
        {
            currentScale -= Time.deltaTime * speed;
            if (currentScale <= minScale)
            {
                currentScale = minScale;
                isIncreasing = true;
            }
        }

        // Aplicar a escala ao texto
        textMeshPro.transform.localScale = Vector3.one * currentScale;
    }
}
