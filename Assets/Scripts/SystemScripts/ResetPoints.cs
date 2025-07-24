using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPoints : MonoBehaviour
{
    [SerializeField] Score points;
    void Start()
    {
        points = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Score>();
        points.score = 0;
    }

    
}
