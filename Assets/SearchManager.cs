using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchManager : MonoBehaviour
{
    public void TrySearchManager()
    {

        NextSceneScript sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<NextSceneScript>();
        sceneManager.TryChangeScene();
    }
}
