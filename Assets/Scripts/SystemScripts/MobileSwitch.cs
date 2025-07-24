using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSwitch : MonoBehaviour
{
    [SerializeField] private GameObject mobilePrefab; // Asigna el objeto desactivado de la escena
    [SerializeField] private GameObject pointerUI;
    void Start()
    {
        // Detectar si la plataforma es m�vil
        if (Application.isMobilePlatform)
        {
            pointerUI.SetActive(false); // Desactivar el UI del puntero en m�vil
            if (mobilePrefab != null)
            {
                // Activar el objeto en la escena
                mobilePrefab.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se ha asignado el objeto m�vil en el inspector.");
            }
        }
    }
}
