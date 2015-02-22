using UnityEngine;
using System.Collections;

public class ShieldBehavior : MonoBehaviour {

	public enum ShieldType
	{
		PLAYER,
		ENEMY
	};
	public ShieldType shieldType;

	public float shieldStrength;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		switch (shieldType)
		{
			case ShieldType.PLAYER:
				if (shieldStrength > 0 && other.CompareTag("EnemyBullet"))
				{
					Destroy(other.gameObject);
					shieldStrength -= 0.5f;
				}
				break;
			case ShieldType.ENEMY:
				if (shieldStrength > 0 && other.CompareTag("PlayerBullet"))
				{
					Destroy(other.gameObject);
					shieldStrength -= 0.5f;
				}
				break;
		}
	}
}
