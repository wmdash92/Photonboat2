using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCtrl : MonoBehaviour
{



    private Rigidbody rb;
    public GameObject expEffect;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 800.0f);
    }







    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision coll)
    {
        GameObject obj = Instantiate(expEffect,
                                    transform.position,
                                    Quaternion.identity);

        Destroy(this.gameObject);
        Destroy(obj, 3.0f);
    }






}
