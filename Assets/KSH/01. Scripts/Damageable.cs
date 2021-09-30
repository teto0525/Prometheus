using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Damageable : MonoBehaviour
{
    [Tooltip("Explosion")]
    public GameObject exploVFX;
    public float exploRadius = 1.0f;
    Rigidbody rb;

    [Tooltip("Damaged")]
    int hitCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(++hitCount == 10)
        {
            Instantiate(exploVFX, transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(gameObject);
        }
    }

}

