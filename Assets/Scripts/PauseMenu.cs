using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenuUI;
    private StarterAssetsInputs starterAssetsInputsScript;
    private FirstPersonController firstPersonControllerScript;
    [SerializeField] private GlobalVariables globalVariables;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Solo permitir pausar si no estamos en MainMenu ni en FinalScreen
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu" || currentScene == "FinalScreen")
            return;
        if (globalVariables != null && globalVariables.isTalking)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void UpdatePlayerReference()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            starterAssetsInputsScript = playerObj.GetComponent<StarterAssetsInputs>();
            firstPersonControllerScript = playerObj.GetComponent<FirstPersonController>();
        }
        else
        {
            starterAssetsInputsScript = null;
            firstPersonControllerScript = null;
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        UpdatePlayerReference();
        if (starterAssetsInputsScript != null)
        {
            starterAssetsInputsScript.enabled = false;
        }
        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = false;
        }

        // Mostrar el cursor y desbloquearlo
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        UpdatePlayerReference();
        if (starterAssetsInputsScript != null)
        {
            starterAssetsInputsScript.enabled = true;
        }
        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = true;
        }

        // Ocultar el cursor y bloquearlo
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UpdatePlayerReference();
        if (starterAssetsInputsScript != null)
        {
            starterAssetsInputsScript.enabled = true;
        }
        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = true;
        }
        // No modificar el cursor aquí
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
