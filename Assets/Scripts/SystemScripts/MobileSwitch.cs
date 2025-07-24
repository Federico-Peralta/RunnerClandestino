using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSwitch : MonoBehaviour
{
    [SerializeField] private GameObject mobilePrefab; // Asigna el objeto desactivado de la escena
    [SerializeField] private GameObject pointerUI;
    void Start()
    {
        // Detectar si la plataforma es móvil
        if (Application.isMobilePlatform)
        {
            pointerUI.SetActive(false); // Desactivar el UI del puntero en móvil
            if (mobilePrefab != null)
            {
                // Activar el objeto en la escena
                mobilePrefab.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se ha asignado el objeto móvil en el inspector.");
            }
        }
    }
}
