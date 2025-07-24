using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform[] grabPoints; // Lista de puntos de agarre
    [SerializeField] private LayerMask pickUpLayerMask;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // Asigna el AudioClip en el inspector

    private Dictionary<Transform, PickableItem> occupiedGrabPoints = new Dictionary<Transform, PickableItem>();

    private void Start()
    {
        if (grabPoints == null || grabPoints.Length == 0)
        {
            grabPoints = new Transform[24]; // 8 columnas x 3 filas
            Camera cam = playerCameraTransform.GetComponent<Camera>();
            if (cam == null)
            {
                cam = Camera.main;
            }

            int index = 0;
            int columns = 8;
            int rows = 3;

            float xStart = 0.15f; // margen izquierdo
            float xEnd = 0.85f;   // margen derecho
            float xStep = (xEnd - xStart) / (columns - 1);

            for (int col = 0; col < columns; col++)
            {
                float x = xStart + xStep * col;
                for (int row = 0; row < rows; row++)
                {
                    float y = 0.15f + 0.2f * row;
                    float z = 0.45f;

                    Vector3 viewportPos = new Vector3(x, y, z);
                    Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);

                    grabPoints[index] = new GameObject($"GrabPoint_{col}_{row}").transform;
                    grabPoints[index].SetParent(playerCameraTransform);
                    grabPoints[index].position = worldPos;
                    index++;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUp();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropAllObjects();
        }

#if UNITY_ANDROID || UNITY_IOS
        // Detectar toque en pantalla y lanzar raycast desde la posición del toque
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TryPickUpMobile(Input.GetTouch(0).position);
        }
#endif
    }

    public void TryPickUp()
    {
        float pickUpDistance = 2f;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
        {
            HandlePickableHit(raycastHit);
        }
    }

    // Para mobile: raycast desde la posición del toque en pantalla
    public void TryPickUpMobile(Vector2 screenPosition)
    {
        float pickUpDistance = 2f;
        Camera cam = playerCameraTransform.GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
        {
            HandlePickableHit(raycastHit);
        }
    }

    private void HandlePickableHit(RaycastHit raycastHit)
    {
        if (raycastHit.transform.TryGetComponent(out PickableItem newPickableItem))
        {
            if (occupiedGrabPoints.ContainsValue(newPickableItem))
            {
                Debug.Log("Este objeto ya está agarrado.");
                return;
            }

            Transform freeGrabPoint = GetFreeGrabPoint();
            if (freeGrabPoint != null)
            {
                occupiedGrabPoints[freeGrabPoint] = newPickableItem;
                newPickableItem.Grab(freeGrabPoint);

                // Reproducir sonido solo si se agarró el objeto
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("No hay puntos de agarre disponibles.");
            }
        }
    }

    private Transform GetFreeGrabPoint()
    {
        foreach (Transform grabPoint in grabPoints)
        {
            if (!occupiedGrabPoints.ContainsKey(grabPoint))
            {
                return grabPoint;
            }
        }
        return null;
    }

    public void DropAllObjects()
    {
        foreach (var entry in occupiedGrabPoints)
        {
            entry.Value.Drop();
        }
        occupiedGrabPoints.Clear();
    }
}
