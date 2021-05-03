using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Photon.Pun;
using UnityEngine.UI;






public class BoatCtrl : MonoBehaviour, IPunObservable
{







    [Header("이동속도 및 회전속도")]
    [Range(10.0f, 200.0f)]
    public float moveSpeed = 10.0f;
    public float turnSpeed = 100.0f;


    private Transform tr;

    public Transform FishPos;
    public Transform FirePos;

    public GameObject fish;
    public GameObject bullet;

    private PhotonView pv;
    public int hp = 100;

    public Image hpBar;






    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();





        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -5.0f, 0);


        if (pv.IsMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = tr.Find("BoatCam").transform;
            GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -5.0f, 0);

        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }





    }









    // Update is called once per frame
    void Update()
    {
            hpBar.fillAmount = hp / 100.0f;

        if (pv.IsMine)
        {



            if (hp <= 0)
            {
                BoatDestroy();
            }
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            tr.Translate(Vector3.forward * Time.deltaTime * moveSpeed * v);
            tr.Rotate(Vector3.up * Time.deltaTime * 100.0f * h);


            if (Input.GetMouseButtonDown(0))
            {
                pv.RPC("Fire", RpcTarget.AllViaServer);
            }
            if (Input.GetMouseButtonDown(1))
            {
                pv.RPC("FishThrow", RpcTarget.AllViaServer);
            }
        }

        else
        {
            if ((tr.position - receivePos).sqrMagnitude > 3.0f * 3.0f)
            {
                tr.position = receivePos;
            }
            else
            {
                tr.position = Vector3.Lerp(tr.position, receivePos, Time.deltaTime * 10.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, receiveRot, Time.deltaTime * 10.0f);
            }
        }


    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Whale"))
        {
            hp -= 110;
        }
        if (coll.collider.CompareTag("Bullet"))
        {
            Debug.Log("hp-10");
            hp -= 10;

        }
    }


    void BoatDestroy()
    {

        Debug.Log("hp 0");

        Vector3 pos = new Vector3(Random.Range(-100.0f, 100.0f),
                                    33.0f,
                                    Random.Range(-180.0f, -220.0f));

        transform.position = pos;
        hp = 100;

    }






    [PunRPC]
    void FishThrow()
    {
        GameObject _fish = Instantiate(fish, FishPos.position, FishPos.rotation);

    }



    [PunRPC]
    void Fire()
    {
        GameObject _bullet = Instantiate(bullet,
                                            FirePos.position,
                                            FirePos.rotation);
    }

    Vector3 receivePos = Vector3.zero;
    Quaternion receiveRot = Quaternion.identity;





    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }








}
