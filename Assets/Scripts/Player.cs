using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxSpeed;
	public float rotationalForce;
	public float fireRate;
	public float bulletSpeed;

	public bool isFiring;

	public Vector2 moveDirection;

	public Transform shipFront;
	public Transform laserLeft;
	public Transform laserRight;

	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{	
		HandlePlayerControls();

		if (Input.GetKey(KeyCode.Space) && !isFiring)
		{
			isFiring = true;
			StartCoroutine(Fire());
		}
	}

	// Accept player input every time Update is called. Handles movement, turning, and physics halt for the player
	private void HandlePlayerControls()
	{
		moveDirection = (new Vector2(shipFront.position.x, shipFront.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
		
		if (Input.GetAxis("Vertical") > 0)
		{
            float input = Input.GetAxis("Vertical");
			rigidbody2D.AddForce(moveDirection * maxSpeed * input);
		}

		// Do regain control of the ship this will briefly halt all physics effect
		if (Input.GetKeyDown(KeyCode.C))
		{
			rigidbody2D.isKinematic = true;
			rigidbody2D.isKinematic = false;
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			rigidbody2D.AddTorque(-rotationalForce, ForceMode2D.Impulse);
		}
		
		else if (Input.GetKey(KeyCode.A))
		{
			rigidbody2D.AddTorque(rotationalForce,ForceMode2D.Impulse);
		}
	}

	// Fires the player ship's lasers.
	private IEnumerator Fire()
	{
		GameObject bulletLeftInstance = Instantiate(bulletPrefab, laserLeft.position, laserLeft.rotation) as GameObject;
		GameObject bulletRightInstance = Instantiate(bulletPrefab, laserRight.position, laserRight.rotation) as GameObject;

		audio.Play();
		bulletLeftInstance.rigidbody2D.AddForce(moveDirection * bulletSpeed * Time.deltaTime);
		bulletRightInstance.rigidbody2D.AddForce(moveDirection * bulletSpeed * Time.deltaTime);

		yield return new WaitForSeconds(fireRate);
		isFiring = false;
	}
}
