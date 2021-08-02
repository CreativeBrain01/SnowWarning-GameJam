using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public enum eState
{
    TITLE,
    GAME,
    PAUSE,
    GAMEOVER,
    INSTRUCTIONS,
    MENU,
    EXITGAME
}

public class GameController : MonoBehaviour
{

    #region Singleton
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {

        }
    }
    #endregion


    //[Header("Editable values")]
    public eState state = eState.TITLE;
    public GameObject uiObj;

    //Dont touch these variables:
    bool forceOnce = true;
    //InputSystem input;
    private SceneController sceneController;
    //Could cause issues here:
    private MenuController menuController;


    void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        DontDestroyOnLoad(sceneController);
        DontDestroyOnLoad(menuController);
        DontDestroyOnLoad(uiObj);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (state == eState.MENU)
        {
            //turnDisplay.SetActive(false);

            forceOnce = true;
        }

        //Game is running
        if (state == eState.GAME)
        {
            if (forceOnce == true)
            {
                GameSession();

                forceOnce = false;
            }
        }
    }

    public void GameSession()
    {
        //Run once on game start things go here
    }

    /// <summary>
    /// Goes to the next in game scene panel
    /// </summary>
    public void Next()
    {
        //Go to the next section/panel based on position
        //Use scene section int in an if/switch to progress
        sceneController.sceneSection++;
        Debug.Log("Next button pressed");
    }

    private void EndGameScene()
    {
        //Goes to the final panel for the game over
        sceneController.sceneSection++;
    }

    public void loadlevel1()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level1");
        menuController.Disable();
    }
    public void loadlevel2()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level2");
        menuController.Disable();
    }
    public void loadlevel3()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level3");
        menuController.Disable();
    }
    public void loadlevel4()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level4");
        menuController.Disable();
    }
    public void loadlevel5()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level5");
        menuController.Disable();
    }
    public void loadlevel6()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level6");
        menuController.Disable();
    }
    public void loadlevel7()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level7");
        menuController.Disable();
    }
    public void loadlevel8()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level8");
        menuController.Disable();
    }
    public void loadlevel9()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level9");
        menuController.Disable();
    }
    public void loadlevel10()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level10");
        menuController.Disable();
    }
    public void loadlevel11()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level11");
        menuController.Disable();
    }
    public void loadlevel12()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level12");
        menuController.Disable();
    }
    public void loadlevel13()
    {
        GameController.Instance.state = eState.GAME;
        SceneManager.LoadScene("Level13");
        menuController.Disable();
    }
}