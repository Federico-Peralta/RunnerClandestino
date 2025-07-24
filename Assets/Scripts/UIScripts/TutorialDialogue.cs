using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] dialogue;
    public float typingSpeed;
    public string prefix = "Tomas: ";
    public bool finishedFirstDialogue = false;
    public Canvas canvasToToggle;
    private int index;
    public IndicationsScripts showIndications;    
    void OnEnable()
    {
        if (canvasToToggle != null)
            canvasToToggle.gameObject.SetActive(false);
        textComponent.text = string.Empty;
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
        textComponent.text = prefix; // Mostrar el prefijo instantáneamente
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
            finishedFirstDialogue = true;
#if UNITY_ANDROID || UNITY_IOS
        if (canvasToToggle != null)
            canvasToToggle.gameObject.SetActive(true);
#endif
            gameObject.SetActive(false);
            if (finishedFirstDialogue == true)
            {
                showIndications.SetIndication("GrabCups");
            }
        }
    }

}
