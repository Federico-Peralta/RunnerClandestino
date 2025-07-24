using System.Collections;
using UnityEngine;
using TMPro;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalText;
    [TextArea]
    [SerializeField] private string[] textos;
    [SerializeField] private float tiempoEnPantalla = 1f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private GameObject objetoFinal;

    private void Awake()
    {
        // Siempre desactiva el objeto al iniciar
        if (objetoFinal != null)
            objetoFinal.SetActive(false);
    }

    private void Start()
    {
        if (finalText != null && textos != null && textos.Length > 0)
        {
            StartCoroutine(MostrarTextos());
        }
        else
        {
            // Si no hay textos, deja el texto vacío y activa el objeto final
            if (finalText != null)
                finalText.text = "";
            if (objetoFinal != null)
                objetoFinal.SetActive(true);
        }
    }

    private IEnumerator MostrarTextos()
    {
        for (int index = 0; index < textos.Length; index++)
        {
            finalText.text = textos[index];
            yield return StartCoroutine(FadeText(0f, 1f, fadeDuration));
            yield return new WaitForSeconds(tiempoEnPantalla);
            yield return StartCoroutine(FadeText(1f, 0f, fadeDuration));
        }
        finalText.text = "";

        // Activa el objeto solo al final
        if (objetoFinal != null)
            objetoFinal.SetActive(true);
    }

    private IEnumerator FadeText(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = finalText.color;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            finalText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        finalText.color = new Color(color.r, color.g, color.b, to);
    }
}
