using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanitationTimer : MonoBehaviour
{
    public Slider sanitationLevel;
    public float timeInSeconds = 60f;
    public GameObject uncuredPatients;
    public GameObject curedPatients;

    void Start()
    {
        sanitationLevel.maxValue = timeInSeconds;
    }

    
    void Update()
    {
        if(uncuredPatients.transform.childCount != 0)
        {
            timeInSeconds -= Time.deltaTime;
            sanitationLevel.value = timeInSeconds;
        }
        else
        {
            // LEVEL COMPLETED!
            float per = Mathf.FloorToInt((sanitationLevel.value / sanitationLevel.maxValue) * 100);
            int totalPats = curedPatients.transform.childCount + uncuredPatients.transform.childCount;
            FindObjectOfType<GameManager>().EndGame(1, per, curedPatients.transform.childCount, totalPats);
        }
        if (uncuredPatients.transform.childCount != 0 && sanitationLevel.value == 0)
        {
            // GAME OVER!
            float per = Mathf.FloorToInt((sanitationLevel.value / sanitationLevel.maxValue) * 100);
            int totalPats = curedPatients.transform.childCount + uncuredPatients.transform.childCount;
            FindObjectOfType<GameManager>().EndGame(0, per, curedPatients.transform.childCount, totalPats);
        }
    }

}
