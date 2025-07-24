using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score = 0f;
    public void AddScore(float points)
    {
        score += points;
    }
}
