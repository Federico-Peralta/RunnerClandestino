using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger con: " + other.gameObject.name);
    }

}
