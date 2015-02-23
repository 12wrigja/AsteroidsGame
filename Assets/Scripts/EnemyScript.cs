using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float maxSpeed;
    public float rotationalForce;
    public float health;
    public float bulletDamage;
    public float rateOfFire;

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        Seek();
    }

    void Seek()
    {
        Vector3 vel3d = target.transform.position - this.transform.position;
        Vector2 vel2d = new Vector2(vel3d.x, vel3d.y);
        vel2d.Normalize();
        vel2d *= maxSpeed;
        this.rigidbody2D.velocity = vel2d;
        float z = Mathf.Atan2((target.transform.position.y - transform.position.y),(target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
    }

}
