using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{




    private Rigidbody rb;
    public GameObject expEffect;












    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 1200.0f);
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision coll)
    {
        GameObject obj = Instantiate(expEffect,
                                    transform.position,
                                    Quaternion.identity);

        Destroy(this.gameObject);
        Destroy(obj, 3.0f);
    }
}
