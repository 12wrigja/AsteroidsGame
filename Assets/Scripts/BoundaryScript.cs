using UnityEngine;
using System.Collections;

public class BoundaryScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet")) 
		{
			Destroy(other.gameObject);
		}
		else if (other.CompareTag("Shield") || other.CompareTag("Asteroid"))
		{
			// Do nothing
		}
		else if (other.CompareTag("AsteroidOutOfBounds"))
		{
			other.transform.parent.position = new Vector2(other.transform.position.x, other.transform.position.y) * -1;
			//other.rigidbody2D.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(1000f, 2000f));
		}
		else
		{
			Debug.Log(other.name + " is out of bounds.");
			other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y) * -0.75f;
		}
	}
}
