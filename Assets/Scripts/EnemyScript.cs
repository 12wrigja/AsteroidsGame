﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float maxSpeed;
    public float rotationalForce;
    public float health;
    public float bulletDamage;
    public float rateOfFire;
    public float detectionDistance;

    //public Transform target;

    private Vector2 targetLocation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y, 0), Color.green);
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetLocation = point;
        //if (FindPlayer())
        //{
        //    MoveGivenVector(Seek());
        //}
        MoveGivenVector(Seek());
    }

    bool FindPlayer()
    {
        float distance = (targetLocation - (Vector2)transform.position).magnitude;
        return distance <= detectionDistance;
    }

    Vector2 Seek()
    {
        Vector2 desired_velocity = (targetLocation - (Vector2)transform.position).normalized * maxSpeed;
        Vector2 steering = truncate(desired_velocity - rigidbody2D.velocity,maxSpeed);
        Debug.DrawLine(this.transform.position, targetLocation, Color.green);
        Debug.DrawLine(transform.position, transform.position + new Vector3(steering.x,steering.y), Color.blue);
        return steering;
    }

    Vector2 Flee()
    {
        Vector2 desired_velocity = ((Vector2)transform.position - targetLocation).normalized * maxSpeed;
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
        float angle = (Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg + 270) % 360;
        float absDifference = Mathf.Abs(angle-rigidbody2D.rotation);
        float deltaAngle = Mathf.Min((angle-rigidbody2D.rotation)*rotationalForce*Time.fixedDeltaTime,Mathf.Sign(angle-rigidbody2D.rotation)*rotationalForce*2);
        Debug.Log(deltaAngle);
        rigidbody2D.MoveRotation(rigidbody2D.rotation+deltaAngle);
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
