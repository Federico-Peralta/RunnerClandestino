using System.Collections;
using UnityEngine;
using TMPro;

public class ShowFinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Asigna en el inspector
    [SerializeField] private Score points;
    [SerializeField] private string mensaje = "Puntaje final: "; // Texto a mostrar antes del valor
    [SerializeField] private float fadeDuration = 1f;   // Duración del fade
    [SerializeField] private GameObject objetoFinal;    // Asigna el GameObject a activar al final

    private void OnEnable()
    {
        points = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Score>();

        if (scoreText != null && points != null)
        {
            if (objetoFinal != null)
                objetoFinal.SetActive(false);
            StartCoroutine(FadeInScore());
        }
    }

    private IEnumerator FadeInScore()
    {
        // Inicializa el texto y lo deja invisible
        scoreText.text = mensaje + points.score;
        Color color = scoreText.color;
        color.a = 0f;
        scoreText.color = color;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            scoreText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        scoreText.color = new Color(color.r, color.g, color.b, 1f);

        // Activar el objeto final
        if (objetoFinal != null)
        {
            objetoFinal.SetActive(true);
        }
    }
}
