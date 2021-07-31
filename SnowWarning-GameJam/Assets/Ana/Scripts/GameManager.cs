using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    float gravity;

    public float Gravity { get => -gravity; }

    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }
}
