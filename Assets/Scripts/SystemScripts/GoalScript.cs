using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField] Score points;
    public GlobalVariables globalVariables;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // Se buscará automáticamente por tag

    private void Awake()
    {
        globalVariables = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GlobalVariables>();
        points = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Score>();
        // Buscar el AudioSource por tag "Jorge"
        GameObject jorgeObj = GameObject.FindGameObjectWithTag("Jorge");
        if (jorgeObj != null)
        {
            audioSource = jorgeObj.GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cup") || collision.gameObject.CompareTag("Bottle"))
        {
            points.AddScore(100);

            // Reproducir el audio solo si no está ya reproduciéndose
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            Destroy(collision.gameObject);
            StartCoroutine(DelayedCheck());            
        }
    }

    private IEnumerator DelayedCheck()
    {
        yield return null; // Espera un frame
        globalVariables.CheckCupsAndSetCanChangeScene();
    }
}
