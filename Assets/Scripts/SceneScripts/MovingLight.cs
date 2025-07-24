using System.Collections;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    [SerializeField] private float minY = -30f;
    [SerializeField] private float maxY = 40f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float changeInterval = 2f; // Tiempo entre cambios de destino
    [SerializeField] private float rotationSpeed = 1.5f; // Velocidad de interpolación

    private float targetX;
    private float targetY;
    private float timer = 0f;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            PickNewTarget();
            timer = 0f;
        }

        // Interpola suavemente hacia el nuevo destino
        Vector3 currentEuler = transform.rotation.eulerAngles;
        // Corrige el rango de ángulos para evitar saltos
        float currentX = Mathf.DeltaAngle(0, currentEuler.x);
        float currentY = Mathf.DeltaAngle(0, currentEuler.y);

        float newX = Mathf.LerpAngle(currentX, targetX, Time.deltaTime * rotationSpeed);
        float newY = Mathf.LerpAngle(currentY, targetY, Time.deltaTime * rotationSpeed);

        transform.rotation = Quaternion.Euler(newX, newY, 0f);
    }

    private void PickNewTarget()
    {
        targetX = Random.Range(minX, maxX);
        targetY = Random.Range(minY, maxY);
    }
}
