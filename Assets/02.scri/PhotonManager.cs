using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;






public class PhotonManager : MonoBehaviourPunCallbacks
{



    private readonly string gameVersion = "v1.0";
    private string userId = "New Boat7";
    public TMP_InputField userIdText;
    public TMP_InputField roomNameText;








    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();

    }




    // Start is called before the first frame update
    void Start()
    {

        userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0,100) :00}");
        userIdText.text = userId;
        PhotonNetwork.NickName = userId;

    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("서버에 접속했습니다!!!!!");
        PhotonNetwork.JoinLobby();

    }







    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        {
            Debug.Log("로비에 들어왔습니다!!!!");
        }
    }



    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode}, msg ={message}");
        
        RoomOptions ro = new RoomOptions();
        ro.IsOpen =true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;


        if (string.IsNullOrEmpty(roomNameText.text))
        {
            roomNameText.text = $"ROOM_{Random.Range(1,100):000}";
        }
        
        PhotonNetwork.CreateRoom(roomNameText.text, ro);

    }








    public override void OnCreatedRoom()
    {
        Debug.Log("방을 만들었습니다!!!");
    }



    public override void OnJoinedRoom()
    {
        Debug.Log("방에 들어왔습니다!!!");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);


        // PhotonNetwork.IsMessageQueueRunning = false;

        if(PhotonNetwork.IsMasterClient)
        {

        PhotonNetwork.LoadLevel("Sea");
        }

    }









    public void OnLoginClick()
    {

        if(string.IsNullOrEmpty(userIdText.text))
        {
            userId =  $"USER_{Random.Range(0,100) :00}";
            userIdText.text = userId;
        }

        PlayerPrefs.SetString("USER_ID", userIdText.text);

        PhotonNetwork.NickName = userIdText.text;


        PhotonNetwork.JoinRandomRoom();

    }



    public void OnMakeRoomClick()
    {
                RoomOptions ro = new RoomOptions();
        ro.IsOpen =true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        if (string.IsNullOrEmpty(roomNameText.text))
        {
            roomNameText.text = $"ROOM_{Random.Range(0,100):000}";
        }


        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }












}
