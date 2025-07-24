using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    public bool canChangeScene = false;
    public bool isTutorialScene = false;
    public IndicationsScripts showIndications;
    public bool isTalking = false;
    private void Awake()
    {
        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Desuscribirse para evitar fugas de memoria
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject indicationsObj = GameObject.FindGameObjectWithTag("Indications");
        if (indicationsObj != null)
        {
            showIndications = indicationsObj.GetComponent<IndicationsScripts>();
        }
        else
        {
            showIndications = null;
        }
    }

    public void CheckCupsAndSetCanChangeScene()
    {
        isTutorialScene = SceneManager.GetActiveScene().name == "Tutorial";
        if (isTutorialScene)
        {
            GameObject[] cups = GameObject.FindGameObjectsWithTag("Cup");
            canChangeScene = cups.Length == 0;
            if (canChangeScene == true && showIndications != null)
            {
                showIndications.SetIndication("Leave");
            }
        }
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
