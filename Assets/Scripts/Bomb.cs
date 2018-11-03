using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        Blade blade = other.GetComponent<Blade>();

        if (!blade)
            return;

        FindObjectOfType<GameManager>().GameOver();
    }
}
