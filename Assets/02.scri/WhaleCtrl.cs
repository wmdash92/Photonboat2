using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class WhaleCtrl : MonoBehaviourPunCallbacks
{

    private Transform WhaleTr;
    private Transform playerTr;

    private NavMeshAgent agent;

    private Transform tr;

    private int food = 0;
    private BoatCtrl boatTarget;



    public LayerMask whatBoat;











    // Start is called before the first frame update
    void Awake()
    {

        WhaleTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();





        GameObject playerObj = GameObject.FindGameObjectWithTag("Boat");

        if (playerObj != null)
        {
            playerTr = playerObj.GetComponent<Transform>();
        }

        Debug.Log(playerObj);


    }









    // Update is called once per frame
    void Update()
    {


        if (photonView.Owner.IsMasterClient)
        {
            agent.SetDestination(playerTr.position);
        }

        if (food == 1)
        {
            StartCoroutine(WhaleStop());
        }




        // else
        // {

        //     if ((tr.position - receivePos).sqrMagnitude > 3.0f * 3.0f)
        //     {
        //         tr.position = receivePos;
        //     }
        //     else
        //     {
        //         tr.position = Vector3.Lerp(tr.position, receivePos, Time.deltaTime * 10.0f);
        //         tr.rotation = Quaternion.Slerp(tr.rotation, receiveRot, Time.deltaTime * 10.0f);
        //     }
        // }
    }





    void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.CompareTag("Boat"))
        {
        }
        if (coll.collider.CompareTag("Fish"))
        {
            Debug.Log("Fish");
            food = 1;
        }
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



    IEnumerator WhaleStop()
    {

        if (food == 1)
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(3.0f);
            agent.isStopped = false;
        }

        food = 0;



    }










}
