using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float damage;

    void OnCollision2DEnter()
    {
        Destroy(gameObject);
    }
}
