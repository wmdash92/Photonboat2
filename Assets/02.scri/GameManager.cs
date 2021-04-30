using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourPunCallbacks
{




    private PhotonView pv;
    public static GameManager instance = null;






    void Awake()
    {

        instance = this;

        Vector3 pos = new Vector3(Random.Range(-50.0f, 50.0f),
                                    5.0f,
                                    Random.Range(-50.0f, 50.0f));

        PhotonNetwork.Instantiate("Boat",
                                    new Vector3(0, 35.0f, -25.0f),
                                    Quaternion.identity,
                                    0);




    }




    void Start()
    {
        pv = GetComponent<PhotonView>();

    }






    void Update()
    {
        
    }






    
}
