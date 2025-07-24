using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    public static NextSceneScript Instance { get; private set; }

    public GlobalVariables globalVariables;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        // Singleton: si ya existe una instancia y no es esta, destr�yete
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        globalVariables = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GlobalVariables>();
    }

    public void TryChangeScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            globalVariables.canChangeScene = true;
        }

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        if (globalVariables != null && globalVariables.canChangeScene)
        {
            // Bajar volumen de la m�sica antes de la animaci�n
            AudioSource musicSource = GameObject.FindGameObjectWithTag("MainMusic")?.GetComponent<AudioSource>();
            if (musicSource != null)
            {
                yield return StartCoroutine(FadeOutMusic(musicSource, 1f));
            }

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                transitionAnim.SetTrigger("End");
                globalVariables.isTalking = true;
                yield return new WaitForSeconds(1f); // Espera a que la animaci�n de transici�n termine 
                SceneManager.LoadScene(nextSceneIndex);
                transitionAnim.SetTrigger("Start");
                globalVariables.isTalking = false;
            }
            else
            {
                Debug.Log("No hay m�s escenas en la jerarqu�a.");
            }
        }
        else
        {
            Debug.Log("No se puede cambiar de escena todav�a.");
        }
    }

    private IEnumerator FadeOutMusic(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }
        audioSource.volume = 0f;
    }
}
