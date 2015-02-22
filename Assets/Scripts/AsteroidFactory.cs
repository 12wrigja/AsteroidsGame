using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidFactory : MonoBehaviour {

	public GameObject asteroidPrefab;
	public int asteroidsToSpawn;
	public Transform[] spawnNodes;

	public List<GameObject> activeAsteroids;

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
			activeAsteroids.Add(Instantiate(asteroidPrefab, spawnNodes[i].position, Quaternion.identity) as GameObject);
			activeAsteroids[i].rigidbody2D.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(1000f, 2000f));
			activeAsteroids[i].rigidbody2D.AddTorque(Random.Range(100f, 500f));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
