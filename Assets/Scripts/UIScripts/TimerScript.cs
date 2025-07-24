using System.Collections;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Asigna el TextMeshProUGUI desde el inspector
    [SerializeField] private int startSeconds = 30; // Cambiado de startMinutes a startSeconds
    [SerializeField] private GlobalVariables globalVariables;
    private float timeRemaining;
    private bool timerRunning = false;
    [SerializeField] NextSceneScript nextScene;

    private void Awake()
    {
        globalVariables = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GlobalVariables>();
        nextScene = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<NextSceneScript>();
    }

    private void OnEnable()
    {
        timeRemaining = startSeconds; // Ahora solo 30 segundos
        timerRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (timerRunning && timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        // Mostrar 00:00 al finalizar
        timerText.text = "00:00";
        timerRunning = false;

        if (globalVariables != null)
        {
            globalVariables.canChangeScene = true;
            nextScene.TryChangeScene();
        }
        else
        {
            Debug.LogWarning("No se encontró el script GlobalVariables en la escena.");
        }
    }
}
