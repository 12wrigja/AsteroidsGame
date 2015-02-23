using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float maxSpeed;
    public float rotationalForce;
    public float health;
    public float bulletDamage;
    public float rateOfFire;
    public float detectionDistance;

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y, 0), Color.green);
        if (FindPlayer())
        {
            Seek();
        }
    }

    bool FindPlayer()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        return distance <= detectionDistance;
    }

    void Seek()
    {
        Vector3 vel3d = target.transform.position - this.transform.position;
        Vector2 desiredVelocity = new Vector2(vel3d.x, vel3d.y);
        Vector3 forwardVector = Vector3.Project(desiredVelocity, transform.up);
        if(forwardVector. > 0){
            Debug.DrawLine(this.transform.position, this.transform.position + forwardVector, Color.red);
            this.rigidbody2D.velocity = forwardVector;
        }
        Vector3 localPosition = transform.InverseTransformPoint(target.transform.position);
        float angle = (Mathf.Atan2(localPosition.y, localPosition.x) * Mathf.Rad2Deg +270) % 360;
        float rotateForce = ((angle > 180) ? (-1 * (angle - 180)) : angle) * rotationalForce / 180;
        this.rigidbody2D.AddTorque(rotateForce);
    }

    Vector2 truncate(Vector2 vector, float MaximumMagnitude)
    {
        if (vector.magnitude > MaximumMagnitude)
        {
            return vector.normalized * MaximumMagnitude;
        }
        return vector;
    }
}
