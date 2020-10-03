using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance
    {
        get; private set;
    }

    private List<GameObject> backgrounds;
    private Viking currentViking;
    private VikingManager vikingManager;
    private PlayableDirector timeline;

    public Viking CurrentViking { get { return currentViking; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        backgrounds = new List<GameObject>();
        BackgroundManager.OnBackgroundSpawned += BackgroundCheck;
        backgrounds.Add(FindObjectOfType<BackgroundManager>().gameObject);

        currentViking = FindObjectOfType<Viking>();
        vikingManager = GetComponent<VikingManager>();

        timeline = GetComponent<PlayableDirector>();

        Viking.OnHealthChanged += HealthCheck;
        WinTrigger.OnWinTriggerEntered += CallGameWin;
    }


    private void HealthCheck(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Current health is " + currentHealth + ", so calling game over");
            StartCoroutine(GameOver());
        }
    }

    private void BackgroundCheck(GameObject newBackground)
    {
        backgrounds.Add(newBackground);
        
        if (backgrounds.Count == 15)
        {
            newBackground.GetComponentInChildren<WinTrigger>(true).gameObject.SetActive(true);
        }
        else if (backgrounds.Count % 3 == 0)
        {
            BackgroundTrigger trigger = newBackground.GetComponentInChildren<BackgroundTrigger>();
            vikingManager.GenerateVikings(trigger.transform);
            trigger.vikingTriggerEnabled = true;
        }
        else
        {
            newBackground.GetComponentInChildren<BackgroundTrigger>().vikingTriggerEnabled = false;
        }
        
    }

    // called by viking button
    public void SwitchVikings(int i)
    {
        currentViking.enabled = false;
        currentViking = vikingManager.GetViking(i).GetComponent<Viking>();
        currentViking.GetComponent<Rigidbody2D>().simulated = true;
        currentViking.enabled = true;
        backgrounds[backgrounds.Count - 1].GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().Follow = currentViking.transform;
    }

    private IEnumerator GameOver()
    {
        Debug.Log("Game over.");
        yield return new WaitForSeconds(1f);
        timeline.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

    // ugly quick fix

    private void CallGameWin()
    {
        StartCoroutine(GameWin());
    }

    private IEnumerator GameWin()
    {
        Debug.Log("Game won!");
        timeline.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Outro");
    }

}
