using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public GameObject PausePanel;
    public GameObject InstructionsPanel;
    public GameObject GamePanel;
    public GameObject ScenePanel;
    public GameObject LevelSelectPanel;
    //public GameObject InteractionPanel;

    [Header("Other")]
    public GameController gameController;
    List<GameObject> panels = new List<GameObject>();
    public AudioMixer mixer;
    private AudioController audioController;
    private SceneController sceneController;
    private int playing;
    public GameObject EventSystem;

    private void Start()
    {
        gameObject.SetActive(true);
        panels.Add(MainMenuPanel);
        panels.Add(OptionsPanel);
        panels.Add(CreditsPanel);
        panels.Add(PausePanel);
        panels.Add(InstructionsPanel);
        panels.Add(GamePanel);
        panels.Add(ScenePanel);
        panels.Add(LevelSelectPanel);
        //panels.Add(InteractionPanel);
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        Disable();
        MainMenuPanel.SetActive(true);
        Time.timeScale = 0;
        GameController.Instance.state = eState.TITLE;
    }

    private void OnEnable()
    {
        menuTrackPlayer();
    }

    private void menuTrackPlayer()
    {
        audioController.Play("Track10");
        Debug.Log("Track 10 played");
        playing = 10;
    }

    private void sceneTrackPlayer()
    {
        audioController.Stop("Track" + playing);
        int trackPlay = UnityEngine.Random.Range(0, 2);
        if (trackPlay == 1)
        {
            audioController.Play("Track5");
            playing = 5;
            Debug.Log("Track 5 played");
        }
        else
        {
            audioController.Play("Track9");
            playing = 9;
            Debug.Log("Track 9 played");
        }
    }

    private void gameTrackPlayer()
    {
        audioController.Stop("Track" + playing);
        int trackPlay = UnityEngine.Random.Range(0, 4);
        if (trackPlay == 1)
        {
            audioController.Play("Track1");
            Debug.Log("Track 1 played");
            playing = 1;
        }
        else if (trackPlay == 2)
        {
            audioController.Play("Track2");
            Debug.Log("Track 2 played");
            playing = 2;
        }
        else if (trackPlay == 3)
        {
            audioController.Play("Track3");
            Debug.Log("Track 3 played");
            playing = 3;
        }
        else
        {
            audioController.Play("Track4");
            Debug.Log("Track 4 played");
            playing = 4;
        }
    }

    public void gameOverTrackPlayer()
    {
        audioController.Stop("Track" + playing);
        audioController.Play("Track6");
        Debug.Log("Track 6 played");
        playing = 6;
    }

    public void Disable()
    {
        foreach (GameObject gameObject in panels)
        {
            gameObject.SetActive(false);
        }
    }
    public void StartGame()
    {
        Disable();
        ScenePanel.SetActive(true);
        GameController.Instance.state = eState.MENU;
        sceneController.once = true;
        sceneTrackPlayer();
        Debug.Log("Start Game");
    }

    public void GameplayStart()
    {
        Disable();
        gameTrackPlayer();
        Time.timeScale = 1;
        GameController.Instance.state = eState.GAME;
        GamePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Disable();
        Time.timeScale = 1;
        GamePanel.SetActive(true);
        GameController.Instance.state = eState.GAME;
        Debug.Log("Resume Game");
    }

    public void Options()
    {
        Disable();
        OptionsPanel.SetActive(true);
        Debug.Log("Options menu");
    }

    public void Instructions()
    {
        Disable();
        InstructionsPanel.SetActive(true);
        //GameController.Instance.state = eState.INSTRUCTIONS;
    }

    public void Credits()
    {
        Disable();
        CreditsPanel.SetActive(true);
        Debug.Log("Credits menu");
    }

    public void Back()
    {
        Disable();

        if (GameController.Instance.state == eState.PAUSE)
        {
            BackToPause();
        }
        else
        {
            BackToMenu();
        }
    }

    public void Pause()
    {
        if (GameController.Instance.state == eState.GAME)
        {
            Time.timeScale = 0;
            Disable();
            PausePanel.SetActive(true);
            GameController.Instance.state = eState.PAUSE;
        }
    }

    //public void Interaction()
    //{
    //    if(GameController.Instance.state == eState.GAME)
    //    {
    //        Disable();
    //        InteractionPanel.SetActive(true);
    //    }
    //}

    //Back to main menu
    public void BackToMenu()
    {
        Disable();
        MainMenuPanel.SetActive(true);
        GameController.Instance.state = eState.TITLE;
    }

    //Back to pause menu
    public void BackToPause()
    {
        Disable();
        PausePanel.SetActive(true);
        GameController.Instance.state = eState.PAUSE;
    }

    public void SetLevelMST(float sliderValue)
    {
        mixer.SetFloat("MST", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelBGM(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelAMB(float sliderValue)
    {
        mixer.SetFloat("AMB", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelPitch(float sliderValue)
    {
        mixer.SetFloat("Pitch", sliderValue);
    }

    public void Mute(bool mute)
    {
        if (mute) mixer.SetFloat("MST", -80);
        else mixer.SetFloat("MST", 0);
    }

    

    public void LevelSelect()
    {
        Disable();
        LevelSelectPanel.SetActive(true);
        GameController.Instance.state = eState.MENU;
    }

    public void ResetApplication()
    {
        //Destroy event system
        EventSystem.SetActive(false);
        SceneManager.LoadScene("Menus");
        sceneController.sceneSection = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}