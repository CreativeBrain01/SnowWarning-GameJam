using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPlatform : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(collision.collider.gameObject);
        }
    }

}
