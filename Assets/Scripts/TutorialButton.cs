using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    [SerializeField]
    Texture2D cursor;

    [SerializeField]
    GameObject[] toDeactivate;

    [SerializeField]
    GameObject[] toActivate;

    public void LoadNext()
    {
        foreach (GameObject go in toDeactivate)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in toActivate)
        {
            go.SetActive(true);
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
