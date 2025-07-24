using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueEntry
    {
        public string tag;                // Tag del objeto a detectar
        public GameObject dialogueObject; // GameObject de diálogo a activar
    }

    [SerializeField] private DialogueEntry[] dialogues;

    private Camera mainCamera;
    private bool[] canActivate;

    private void Awake()
    {
        mainCamera = Camera.main;
        canActivate = new bool[dialogues.Length];
        for (int i = 0; i < canActivate.Length; i++)
            canActivate[i] = true;
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            TryActivateDialogue(touchPos);
        }
#else
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 screenPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
            TryActivateDialogue(screenPos);
        }
#endif
    }

    private void TryActivateDialogue(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Si el primer diálogo (A) está activo, no permitir activar otros diálogos
            if (dialogues.Length > 0 && dialogues[0].dialogueObject != null && dialogues[0].dialogueObject.activeInHierarchy)
            {
                // Solo permitir reactivar el primer diálogo si corresponde
                return;
            }

            for (int i = 0; i < dialogues.Length; i++)
            {
                var entry = dialogues[i];
                if (canActivate[i] && hit.transform.CompareTag(entry.tag) && entry.dialogueObject != null)
                {
                    entry.dialogueObject.SetActive(true);

                    if (i == 0)
                    {
                        canActivate[i] = false;
                    }
                    break;
                }
            }
        }
    }


}
