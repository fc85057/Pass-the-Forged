using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VikingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject vikingPrefab;
    [SerializeField]
    private GameObject selectionScreen;
    [SerializeField]
    private VikingButton[] vikingButtons;

    private List<GameObject> vikings;

    private void Start()
    {
        vikings = new List<GameObject>();

        BackgroundTrigger.OnVikingTrigger += EnableSelectionScreen;
    }

    public GameObject GetViking(int i)
    {
        selectionScreen.SetActive(false);
        Time.timeScale = 1f;
        return vikings[i];
    }

    public void GenerateVikings(Transform transform)
    {

        vikings.Clear();

        for (int i = 0; i < 3; i++)
        {
            Vector2 spawnPosition = new Vector2(transform.position.x + 5+i, transform.position.y);
            GameObject newViking = Instantiate(vikingPrefab, spawnPosition, Quaternion.identity);
            newViking.GetComponent<Viking>().enabled = false;
            newViking.GetComponent<Rigidbody2D>().simulated = false;
            VikingStats newStats = GenerateStats();
            newViking.GetComponent<Viking>().SetStats(newStats);
            SpriteRenderer[] srs = newViking.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
            {
                sr.color = Random.ColorHSV();
            }

            vikings.Add(newViking);
            vikingButtons[i].SetStats(newViking.GetComponent<Viking>().Stats);
        }

    }


    private VikingStats GenerateStats()
    {
        VikingStats newStats = (VikingStats)ScriptableObject.CreateInstance("VikingStats");
        newStats.maxHealth = Round(Random.Range(40, 101));
        newStats.maxStamina = Round(Random.Range(40, 101));
        newStats.healing = Round(Random.Range(40, 101));
        newStats.meleeDamage = Round(Random.Range(5, 21));
        newStats.rangeDamage = Round(Random.Range(5, 21));
        newStats.immunities = GetElements();
        return newStats;
    }



    private int Round(int numberToRound)
    {
        return ((int)Mathf.Round(numberToRound / 5.0f)) * 5;
    }

    private Element[] GetElements()
    {
        // System.Array allElements = System.Enum.GetValues(typeof(Element));
        Element[] allElements = (Element[])System.Enum.GetValues(typeof(Element));
        int numberOfElements = Random.Range(0, 4);
        HashSet<Element> immunitiesSet = new HashSet<Element>();
        for (int i = 0; i < numberOfElements + 1; i++)
        {
            immunitiesSet.Add(allElements[Random.Range(0, allElements.Length)]);
        }
        Element[] immunities = immunitiesSet.ToArray();
        return immunities;
    }

    private void EnableSelectionScreen()
    {
        
        selectionScreen.SetActive(true);
        Time.timeScale = 0f;
    }

}
