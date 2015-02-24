using UnityEngine;
using System.Collections;

public class ShieldBehavior : MonoBehaviour {

	public enum ShieldType
	{
		PLAYER,
		ENEMY
	};
	public ShieldType shieldType;

    public float maxShieldStrength;
	public float shieldStrength;

	// Use this for initialization
	void Start () 
	{
        shieldStrength = maxShieldStrength;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (shieldStrength < 0)
        {
            this.enabled = false;
        }
	}

	void OnTriggerEnter(Collider other)
	{
		switch (shieldType)
		{
			case ShieldType.PLAYER:
				if (shieldStrength > 0 && other.CompareTag("EnemyBullet"))
				{
					Destroy(other.gameObject);
                    shieldStrength -= other.GetComponent<BulletScript>().damage;
				}
				break;
			case ShieldType.ENEMY:
				if (shieldStrength > 0 && other.CompareTag("PlayerBullet"))
				{
                    Debug.Log("Damaging Enemy");
					Destroy(other.gameObject);
                    shieldStrength -= other.GetComponent<BulletScript>().damage;
				}
				break;
		}
	}

    void OnCollisionEnter(Collision collider)
    {
        Debug.Log(collider.gameObject.name);
    }
}
