using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sprite;

    public IEnumerator FadeIn(int index)
    {
        anim.SetBool("FadeIn", true);
        yield return new WaitUntil(() => sprite.color.a == 1);
        SceneManager.LoadScene(index);
    }
}
