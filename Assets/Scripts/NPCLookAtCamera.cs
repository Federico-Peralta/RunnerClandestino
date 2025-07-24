using UnityEngine;

public class NPCLookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float rotationSpeed = 5f; // Velocidad de giro

    void Update()
    {
        if (targetCamera == null) return;

           
        Vector3 targetPosition = targetCamera.transform.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = (targetPosition - transform.position).normalized;

         if (direction.sqrMagnitude > 0.001f)
         {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
            );
         }
    }
}
