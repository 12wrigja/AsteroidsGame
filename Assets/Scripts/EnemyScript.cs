using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float maxSpeed;
    public float rotationalForce;
    public float health;
    public float bulletDamage;
    public float bulletSpeed;
    public float fireWait;
    public float detectionDistance;

    public GameObject target;

    public bool isFiring;
    public Transform laserLeft;
    public Transform laserRight;

    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float attackAngle = Vector3.Angle(transform.up, (target.transform.position - transform.position));
        if (attackAngle < 10 && !isFiring)
        {
            FireLaser();
        }
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
            float deltaAngle = angle * rotationalForce * Time.deltaTime;
            rigidbody2D.MoveRotation(rigidbody2D.rotation + deltaAngle);
    }

    Vector2 truncate(Vector2 vector, float MaximumMagnitude)
    {
        if (vector.magnitude > MaximumMagnitude)
        {
            return vector.normalized * MaximumMagnitude;
        }
        return vector;
    }

    private void FireLaser()
    {
        isFiring = true;
        StartCoroutine(Fire());
    }

    // Fires the enemy ship's lasers.
    private IEnumerator Fire()
    {
        GameObject bulletLeftInstance = Instantiate(bulletPrefab, laserLeft.position, laserLeft.rotation) as GameObject;
        GameObject bulletRightInstance = Instantiate(bulletPrefab, laserRight.position, laserRight.rotation) as GameObject;

        audio.Play();
        bulletLeftInstance.rigidbody2D.AddForce(transform.up * bulletSpeed * Time.deltaTime);
        bulletRightInstance.rigidbody2D.AddForce(transform.up * bulletSpeed * Time.deltaTime);

        yield return new WaitForSeconds(fireWait);
        Debug.Log("Resetting Enemy Guns");
        isFiring = false;
    }
}
