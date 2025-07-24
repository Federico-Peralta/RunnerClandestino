using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FinalDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] dialogue;
    public float typingSpeed;
    public string prefix = "Tomás: ";
    public Canvas canvasToToggle; // Canvas a desactivar/activar
    private int index;
    [SerializeField] private GlobalVariables globalVariables;
    [SerializeField] NextSceneScript nextScene;

    void OnEnable()
    {
        if (canvasToToggle != null)
            canvasToToggle.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        globalVariables = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GlobalVariables>();
        nextScene = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<NextSceneScript>();
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
            StartCoroutine(TypeDialogue());
        }
        else
        {
#if UNITY_ANDROID || UNITY_IOS
        if (canvasToToggle != null)
            canvasToToggle.gameObject.SetActive(true);
#endif
            globalVariables.canChangeScene = true;
            nextScene.TryChangeScene();
            gameObject.SetActive(false);
        }
    }


}
