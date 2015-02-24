using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidFactory : MonoBehaviour {

	public GameObject asteroidPrefab;
	public int asteroidsToSpawn;
	public Transform[] spawnNodes;

	public static List<GameObject> activeAsteroids = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		GameObject[] spawnNodeGameObjects = GameObject.FindGameObjectsWithTag("SpawnNode");
		spawnNodes = new Transform[spawnNodeGameObjects.Length];

		for (int i = 0; i < spawnNodeGameObjects.Length; i++)
		{
			spawnNodes[i] = spawnNodeGameObjects[i].transform;
		}

		for (int i = 0; i < spawnNodes.Length; i++)
		{
            SpawnAsteroid(spawnNodes[i].position);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        activeAsteroids.RemoveAll(item => item == null);
        if (activeAsteroids.Count < asteroidsToSpawn)
        {
            SpawnAsteroid(spawnNodes[Random.Range(0, spawnNodes.Length)].position);
        }
	}

    void SpawnAsteroid(Vector3 position)
    {
        GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity) as GameObject;
        activeAsteroids.Add(asteroid);
        asteroid.rigidbody2D.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(1000f, 2000f));
        asteroid.rigidbody2D.AddTorque(Random.Range(100f, 500f));
    }
}
