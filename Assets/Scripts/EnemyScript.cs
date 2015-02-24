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
        if (target != null)
        {
            MoveGivenVector(Seek());
        }
        
    }

    bool FindPlayer()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        return distance <= detectionDistance;
    }

    Vector2 Seek()
    {
        Vector2 desired_velocity = (target.transform.position - transform.position).normalized * maxSpeed;
        Vector2 steering = truncate(desired_velocity - rigidbody2D.velocity,maxSpeed);
        Debug.DrawLine(this.transform.position, target.transform.position, Color.green);
        Debug.DrawLine(transform.position, transform.position + new Vector3(steering.x,steering.y), Color.blue);
        return steering;
    }

    Vector2 Flee()
    {
        Vector2 desired_velocity = (transform.position - target.transform.position).normalized * maxSpeed;
        Vector2 steering = desired_velocity - rigidbody2D.velocity;
        return steering;
    }

    void MoveGivenVector(Vector2 movementVector)
    {
        Debug.DrawLine(this.transform.position, transform.position + (Vector3)movementVector, Color.grey);
        Vector3 forwardVector = truncate(Vector3.Project(movementVector, transform.up), maxSpeed);
        if (Vector3.Angle(forwardVector, transform.up) < 15)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + forwardVector, Color.red);
            rigidbody2D.AddForce(forwardVector);
        }
            float angle = (Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg + 270) % 360 - rigidbody2D.rotation;
            if (angle < -180)
            {
                angle += 360;
            }
            Debug.Log(angle);
            rigidbody2D.AddTorque(rotationalForce * angle / 180);
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
