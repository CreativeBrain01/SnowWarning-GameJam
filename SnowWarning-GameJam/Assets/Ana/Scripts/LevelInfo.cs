using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] public Vector2 startPosition;

    public void Start()
    {
        GameManager.Instance.levelInfo = this;
    }
}
