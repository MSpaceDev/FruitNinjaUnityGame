using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public GameObject slicedFruit;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void SliceFruit()
    {
        GameObject slices = Instantiate(slicedFruit, transform.position, transform.rotation);

        Rigidbody[] rbs = slices.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in rbs)
        {
            rb.gameObject.transform.rotation = Random.rotation;
            rb.AddExplosionForce(Random.Range(500, 1000), slices.transform.position, 5);
        }

        gameManager.IncreaseScore(3);
        gameManager.PlaySliceSound();

        Destroy(slices, 5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade blade = collision.GetComponent<Blade>();

        if (!blade)
            return;

        SliceFruit();
    }
}
