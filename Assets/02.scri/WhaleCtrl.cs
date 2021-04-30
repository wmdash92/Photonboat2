using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhaleCtrl : MonoBehaviour
{

    private Transform monsterTr;
    private Transform playerTr;

    private NavMeshAgent agent;













    // Start is called before the first frame update
    void Start()
    {
    
    monsterTr = GetComponent<Transform>();
    GameObject playerObj = GameObject.FindGameObjectWithTag("Boat");
     if (playerObj != null) // if(playerObj)
        {
            playerTr = playerObj.GetComponent<Transform>();
        }
    
    agent = GetComponent<NavMeshAgent>();

    }









    // Update is called once per frame
    void Update()
    {
    
        agent.SetDestination(playerTr.position);  // 함수(메소드)를 사용

    }


    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.CompareTag("Boat"))
        {
        }
        if(coll.collider.CompareTag("Fish"))
        {

        }



    }




}
