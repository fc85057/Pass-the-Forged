using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    private void Start()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
