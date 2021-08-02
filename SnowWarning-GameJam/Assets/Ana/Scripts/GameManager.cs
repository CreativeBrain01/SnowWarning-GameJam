using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public GameObject player;

    public LevelInfo levelInfo { get; set; }

    public static int nextIndex { get => SceneManager.GetActiveScene().buildIndex + 1; }

    public enum eState
    {
        Menu,
        LevelStart,
        Level,
        PlayerDead,
        LevelEnd,
        Transition
    }

    public static eState state { get; set; } = eState.Level;

    public GameObject transition;

    public static bool play;

    public bool spawnPlayer;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transition);
    }

    void Start()
    {
        try
        {
            levelInfo = FindObjectOfType<LevelInfo>();
        }
        catch
        {
            return;
        }
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
                spawnPlayer = true;
                if(FindObjectOfType<Player>() != null)
                {
                    state = eState.Level;
                }
                break;
            case eState.Level:
                Time.timeScale = 1;
                play = true;
                if (FindObjectOfType<Player>() == null)
                {
                    state = eState.PlayerDead;
                }
                break;
            case eState.PlayerDead:
                Time.timeScale = 1;
                if (spawnPlayer)
                {
                    play = false;
                    StartCoroutine(transition.GetComponent<Transition>().FadeIn());
                    state = eState.Transition;
                }
                break;
            case eState.LevelEnd:
                Time.timeScale = 1;
                spawnPlayer = false;
                play = false;
                StartCoroutine(transition.GetComponent<Transition>().FadeIn(nextIndex));
                state = eState.Transition;
                break;
            case eState.Transition:
                break;
            default:
                break;
        }
    }


    public void RespawnPlayer()
    {
        Instantiate(player, levelInfo.startPosition, Quaternion.identity);
        state = eState.LevelStart;
    }
}
