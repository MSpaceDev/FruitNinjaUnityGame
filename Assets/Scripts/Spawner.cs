using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] fruits;
    public Transform[] spawnPlaces;

    public GameObject bomb;

    public float minWait = .3f;
    public float maxWait = 1.0f;
    public int minForce = 12;
    public int maxForce = 17;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnFruits());
	}
	
	IEnumerator SpawnFruits()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            GameObject fruit = fruits[Random.Range(0, fruits.Length)];
            Transform spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject objectToSpawn = Random.Range(0.0f, 1.0f) < 0.1f ? bomb : fruit;
            GameObject fruitSpawn = Instantiate(objectToSpawn, spawnPlace.position, spawnPlace.rotation);

            fruitSpawn.GetComponent<Rigidbody2D>().AddForce(fruitSpawn.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            fruitSpawn.transform.rotation = Random.rotation;

            Destroy(fruitSpawn, 5);
        }
    }
}
