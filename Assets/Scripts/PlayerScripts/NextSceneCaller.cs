using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextSceneCaller : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] NextSceneScript nextSceneScript;

    private void Awake()
    {
        mainCamera = Camera.main;
        nextSceneScript = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<NextSceneScript>();
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        // Mobile: detectar toque en pantalla
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Verifica si el toque está sobre la UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            Vector2 touchPos = Input.GetTouch(0).position;
            TryCallNextScene(touchPos);
        }
#else
        // PC: detectar tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 screenPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
            TryCallNextScene(screenPos);
        }
#endif
    }

    private void TryCallNextScene(Vector2 screenPosition)
    {
        float raycastRange = 2f;
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastRange))
        {
            if (hit.transform.CompareTag("ExitDoor"))
            {
                if (nextSceneScript != null)
                {
                    nextSceneScript.TryChangeScene();
                }
            }
        }
    }
}
