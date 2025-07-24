using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoints : MonoBehaviour
{
    public int points;

    private void Awake()
    {
        points = 0;
    }

    public void ChangePoints()
    {
        points += 45;
    }
}
