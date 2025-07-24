using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
