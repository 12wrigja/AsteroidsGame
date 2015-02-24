using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour {

	public GameObject asteroidBigPrefab;
	public GameObject asteroidMidPrefab;
	public GameObject asteroidSmallPrefab;
	
	public float asteroidMidTorqueMax;
	public float asteroidSmallTorqueMax;

	public enum AsteroidClass
	{
		BIG,
		MID,
		SMALL
	};
	public AsteroidClass asteroidClass;

	void Start ()
	{
		asteroidMidTorqueMax = 300f;
		asteroidSmallTorqueMax = 20f;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("PlayerBullet") || col.gameObject.CompareTag("EnemyBullet"))
		{
			Destroy(col.gameObject);

			switch (asteroidClass)
			{
			case AsteroidClass.BIG:
				Destroy(gameObject);
				for (int i = 0; i < 2; i++)
				{
					GameObject newAsteroid = Instantiate(asteroidMidPrefab, transform.position, Quaternion.identity) as GameObject;
					newAsteroid.rigidbody2D.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(500f, 1000f));
					newAsteroid.rigidbody2D.AddTorque(Random.Range(-asteroidMidTorqueMax, asteroidMidTorqueMax));
                    AsteroidFactory.activeAsteroids.Add(newAsteroid);
				}
				break;
			case AsteroidClass.MID:
				Destroy(gameObject);
				for (int i = 0; i < 3; i++)
				{
					GameObject newAsteroid = Instantiate(asteroidSmallPrefab, transform.position, Quaternion.identity) as GameObject;
					newAsteroid.rigidbody2D.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(250f, 500f));
					newAsteroid.rigidbody2D.AddTorque(Random.Range(-asteroidSmallTorqueMax, asteroidSmallTorqueMax));
                    AsteroidFactory.activeAsteroids.Add(newAsteroid);
				}
				break;
			case AsteroidClass.SMALL:
				Destroy(gameObject);
				break;
			}
		}
	}
}
