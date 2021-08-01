using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public static int index { get => SceneManager.GetActiveScene().buildIndex; }

    public enum eState
    {
        Menu,
        LevelStart,
        Level,
        PlayerDead,
        LevelEnd
    }

    public eState state { get; set; }

    public Transition transition;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        switch (state)
        {
            case eState.Menu:
                Time.timeScale = 0;
                break;
            case eState.LevelStart:
                Time.timeScale = 1;
                break;
            case eState.Level:
                Time.timeScale = 1;
                break;
            case eState.PlayerDead:
                Time.timeScale = 1;
                break;
            case eState.LevelEnd:
                Time.timeScale = 1;

                break;
            default:
                break;
        }
    }
}
