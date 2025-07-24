using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IndicationsScripts : MonoBehaviour
{
    [SerializeField] private RectTransform panel; // Asigna el panel desde el inspector
    [SerializeField] private TextMeshProUGUI indicationText; // Asigna el TextMeshProUGUI desde el inspector
    [SerializeField] private Canvas timerCanvas; // Asigna el canvas del timer desde el inspector

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Tutorial")
        {
            SetIndication("Tutorial");
        }
        else
        {
            CheckSceneAndSetIndication();
        }
    }

    public void CheckSceneAndSetIndication()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Level1":
                SetIndication("Level1");
                break;
            case "Level2":
                SetIndication("Level1");
                break;
            case "Level3":
                SetIndication("Level1");
                break;
            case "Level4":
                SetIndication("Tutorial");
                break;
            default:
                SetIndication("");
                break;
        }
    }

    public void StartPanelAnimation()
    {
        StartCoroutine(MovePanelSequence());
    }

    private IEnumerator MovePanelSequence()
    {
        yield return StartCoroutine(MovePanelY(panel, panel.anchoredPosition.y, -60f, 0.5f));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(MovePanelY(panel, panel.anchoredPosition.y, 60f, 0.5f));
    }

    private IEnumerator MovePanelY(RectTransform rect, float fromY, float toY, float duration)
    {
        float elapsed = 0f;
        Vector2 start = rect.anchoredPosition;
        Vector2 end = new Vector2(start.x, toY);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rect.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }
        rect.anchoredPosition = end;
    }

    // Cambia el texto según la clave recibida
    public void SetIndication(string key)
    {
        switch (key)
        {
            case "GrabCups":
                indicationText.text = "Recoge los vasos y sueltalos en la mesa de Jorge";
                StartPanelAnimation();
                break;
            case "Leave":
                indicationText.text = "Vete del lugar";
                StartPanelAnimation();
                Debug.Log("Leave activado");
                break;
            case "Tutorial":
                indicationText.text = "Habla con tu amigo Tomas";
                StartPanelAnimation();
                break;
            case "Level1":
                indicationText.text = "Habla con Jorge";
                StartPanelAnimation();
                break;
            case "Level":
                indicationText.text = "Recoge la mayor cantidad de vasos y botellas que puedas!";
                StartPanelAnimation();
                StartCoroutine(ActivateTimerAfterDelay(2f));
                break;
            default:
                indicationText.text = "";
                break;
        }
    }

    private IEnumerator ActivateTimerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (timerCanvas != null)
            timerCanvas.gameObject.SetActive(true);
    }

}
