using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    private Quaternion rotationOffset = Quaternion.identity;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform newGrabPointTransform)
    {
        objectGrabPointTransform = newGrabPointTransform;

        // Configurar el Rigidbody para evitar conflictos de físicas
        objectRigidBody.useGravity = false;
        objectRigidBody.isKinematic = true;
        objectRigidBody.detectCollisions = false;

        // Definir el offset de rotación según el tag
        if (CompareTag("Bottle"))
        {
            rotationOffset = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            rotationOffset = Quaternion.identity;
        }
    }

    public void Drop()
    {
        objectGrabPointTransform = null;

        // Restaurar el comportamiento del Rigidbody
        objectRigidBody.useGravity = true;
        objectRigidBody.isKinematic = false;
        objectRigidBody.detectCollisions = true;

        // Liberar el objeto del punto de agarre
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            // Movimiento suave usando Lerp
            float lerpSpeed = 30f; // Puedes ajustar este valor para más o menos suavidad
            Vector3 targetPosition = objectGrabPointTransform.position;
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
            objectRigidBody.MovePosition(newPosition);

            // Rotación instantánea con offset
            objectRigidBody.MoveRotation(objectGrabPointTransform.rotation * rotationOffset);
        }
    }

}
