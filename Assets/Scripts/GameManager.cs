using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool hasGameEnded = false;
    public float restartDelay = 1f;

    [Header("Settings Menu")]
    public GameObject settingsMenu;
    public AudioManager audioManager;

    [Header("Help Menu")]
    public GameObject helpMenu;

    [Header("Credits Menu")]
    public GameObject creditsMenu;

    [Header("Game Over! Properties")]
    public GameObject gameOverPanel;
    public Text GOData;
    [Header("Level Complete! Properties")]
    public GameObject levelCompletePanel;
    public Text LCData;

    [Header("Paused Items")]
    public GameObject pausePanel;
    public GameObject patientTracker;
    public GameObject player;
    public GameObject sanitationTimer;
    public GameObject weapons;
    public GameObject weaponSwitching;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.a = 0;
    }

    public void EndGame(int endFlag, float sanLevel, int curedPatients, int totalPatients)
    {
        // endFlag 0 = GAME OVER
        // endFlag 1 = GAME WON
        if(!hasGameEnded)
        {
            hasGameEnded = true;
            if(endFlag == 1)
            {
                LCData.text = "Patients Cured: " + curedPatients + " / " + totalPatients + "\nSanitation Level: " + sanLevel + "%";
                levelCompletePanel.SetActive(true);
            }
            else if(endFlag == 0)
            {
                GOData.text = "Patients Cured: " + curedPatients + " / " + totalPatients + "\nSanitation Level: " + sanLevel + "%";
                gameOverPanel.SetActive(true);
            }

            FindObjectOfType<PlayerMovement>().enabled = false;
        }
    }

    
    public void RestartGame()
    {
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 1;
        pausePanel.SetActive(false);
        // Enable the scripts again
        patientTracker.GetComponent<PatientTracker>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        sanitationTimer.GetComponent<SanitationTimer>().enabled = true;
        weaponSwitching.GetComponent<WeaponSwitching>().enabled = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.a = 0;
    }

    public void QuitGame()
    {
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.b = 0;
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level 1");
        AudioManager.Instance.a = 0;
    }

    public void OpenSettingsMenu()
    {
        //settingsMenu.GetComponentInChildren<Slider>().value = AudioManager.Instance.GetComponent<AudioSource>().volume;
        settingsMenu.SetActive(true);
    }

    public void AdjustVolume()
    {
        AudioManager.Instance.GetComponent<AudioSource>().volume = settingsMenu.GetComponentInChildren<Slider>().value;
    }

    public void CloseSettingsMenu()
    {
        //AudioManager.Instance.GetComponent<AudioSource>().volume = settingsMenu.GetComponentInChildren<Slider>().value;
        settingsMenu.SetActive(false);
    }

    public void OpenHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
    }

    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        creditsMenu.SetActive(false);
    }

    public void ExitAndCloseGame()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void PauseCurrentGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        // Disable scripts that still work while timescale is set to 0
        patientTracker.GetComponent<PatientTracker>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        sanitationTimer.GetComponent<SanitationTimer>().enabled = false;
        weaponSwitching.GetComponent<WeaponSwitching>().enabled = false;
    }
    public void ContinueCurrentGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        // Enable the scripts again
        patientTracker.GetComponent<PatientTracker>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        sanitationTimer.GetComponent<SanitationTimer>().enabled = true;
        weaponSwitching.GetComponent<WeaponSwitching>().enabled = true;
    }
}
