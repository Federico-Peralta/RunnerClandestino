using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JorgeDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] dialogue;
    public float typingSpeed;
    public string prefix = "Jorge: ";
    public Canvas canvasToToggle; // Canvas a desactivar/activar
    private int index;
    [SerializeField] private IndicationsScripts showIndications;
    [SerializeField] private GameObject spawners;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GlobalVariables globalVariables;
    bool currentScene = false;
    void OnEnable()
    {
        if (canvasToToggle != null)
            canvasToToggle.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        globalVariables = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GlobalVariables>(); globalVariables.isTalking = true;
        StartDialogue();
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        bool input = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#else
        bool input = Input.GetMouseButtonDown(0);
#endif
        if (input)
        {
            if (textComponent.text == prefix + dialogue[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = prefix + dialogue[index];
            }
        }
    }

    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()
    {
        textComponent.text = prefix;
        foreach (char letter in dialogue[index].ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextLine()
    {
        index++;
        if (index < dialogue.Length)
        {
            textComponent.text = string.Empty;
            globalVariables.isTalking = true;
            StartCoroutine(TypeDialogue());
        }
        else
        {
#if UNITY_ANDROID || UNITY_IOS
        if (canvasToToggle != null)
            canvasToToggle.gameObject.SetActive(true);
#endif
            gameObject.SetActive(false);
            if (currentScene = SceneManager.GetActiveScene().name == "Tutorial")
            {
                showIndications.SetIndication("GrabCups");
                globalVariables.isTalking = false;
                return;
            }
            globalVariables.isTalking = false;
            spawners.SetActive(true);
            showIndications.SetIndication("Level");
            obstacle.SetActive(false);
        }
    }


}
