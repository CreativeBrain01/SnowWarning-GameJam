using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sprite;

    public IEnumerator FadeIn(int index = -1)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => sprite.color.a >= 0.96f);
        if(index > -1)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            GameManager.Instance.RespawnPlayer();
        }
        yield return new WaitUntil(() => sprite.color.a == 0);
        anim.SetBool("Fade", false);
    }
}
