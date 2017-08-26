using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkGame : PunBehaviour
{
    public DataWorker DW;
    public Transform[] spawnPoints;
    public Camera cam;
    private GameObject localPlayerObject;
    //private List<GameObject> localPlayerObject = new List<GameObject>();

    //Player properties
    [Header("Player properties")]
    private string _name;
    private int _gender;
    //private int _id;
    private int _hair;
    private int _eyes;
    private int _hairColor;
    private int _eyesColor;
    //Itens
    [Header("Player Items")]
    private int chest;
    private int hands;
    private int WeaponMain;
    private int Legs;
    private int Boots;
    private int WeaponOff;

    public int state = 0;
    private int assignedUserId;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("V0.1a");

        DW = GameObject.FindGameObjectWithTag("DataWorker").GetComponent<DataWorker>();
        _name = DW._name;
        _gender = DW._gender;
        //_id = DW._id;
        _hair = DW._hair;
        _eyes = 1;
        _hairColor = DW._hairColor;
        _eyesColor = DW._eyesColor;
        chest = DW.chest;
        hands = DW.hands;
        WeaponMain = DW.WeaponMain;
        Legs = DW.Legs;
        Boots = DW.Boots;
        WeaponOff = DW.WeaponOff;
        PhotonNetwork.player.NickName = _name;
    }

    void OnGUI()
    {
        switch (state)
        {
            case 0:
                if (GUI.Button(new Rect(10, 10, 100, 30), "Connect"))
                {
                    CreatGame();
                }
                break;

            case 1:
                GUI.Label(new Rect(10, 40, 100, 30), "Connected", "box");
                if (GUI.Button(new Rect(10, 10, 100, 30), "Search"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }
                break;

            case 2:
                GUI.Label(new Rect(10, 40, 100, 30), "join", "box");
                if (GUI.Button(new Rect(10, 10, 100, 30), "play"))
                {
                    SpawnPlayer(PhotonNetwork.player, _gender, spawnPoints[0]);
                }
                break;
            case 3:
                //in game
                break;
        }
    }

    #region Network
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connect");
    }

    public override void OnJoinedLobby()
    {
        state = 1;
        //Debug.Log("OnJoinedLobby() : Hey, You're in a Lobby ! " + PhotonNetwork.PhotonServerSettings.ServerAddress);
    }

    public void OnPhotonRandomJoinFailed()
    {
        //RoomOptions options = new RoomOptions();
        PhotonNetwork.CreateRoom(null);
    }

    public void CreatGame()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(_name, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        SetCustomProperties(PhotonNetwork.player, _gender, _hair, _hairColor,
                            _eyes, _eyesColor, PhotonNetwork.playerList.Length - 1,
                            chest, hands, WeaponMain, Legs, Boots, WeaponOff);
        state = 2;
    }
    public override void OnJoinedRoom()
    {
        SetCustomProperties(PhotonNetwork.player, _gender, _hair, _hairColor,
                            _eyes, _eyesColor, PhotonNetwork.playerList.Length - 1,
                            chest, hands, WeaponMain, Legs, Boots, WeaponOff);
        state = 2;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        //if (PhotonNetwork.isMasterClient)
        //{
        SetCustomProperties(PhotonNetwork.player, _gender, _hair, _hairColor,
                        _eyes, _eyesColor, PhotonNetwork.playerList.Length - 1,
                        chest, hands, WeaponMain, Legs, Boots, WeaponOff);
        //}

        //Debug.Log("Player Connected " + newPlayer.name);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer disconnectedPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            int playerIndex = 0;
            foreach (PhotonPlayer p in PhotonNetwork.playerList)
            {
                SetCustomProperties(
                   p, (int)p.CustomProperties["gender"], (int)p.CustomProperties["hairIndex"],
                   (int)p.CustomProperties["hairColor"], (int)p.CustomProperties["eyesIndex"],
                   (int)p.CustomProperties["eyesColor"], playerIndex++,
                   (int)p.CustomProperties["chest"], (int)p.CustomProperties["hands"],
                   (int)p.CustomProperties["WeaponMain"], (int)p.CustomProperties["Legs"],
                   (int)p.CustomProperties["Boots"], (int)p.CustomProperties["WeaponOff"]);
            }
        }
        Debug.Log("Player Disconnected " + disconnectedPlayer.NickName);
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    #endregion

    private void SpawnPlayer(PhotonPlayer player, int gender, Transform spawnPoint)
    {
        state = 3;
        cam.enabled = false;
        if (gender == 0)
        {
            localPlayerObject/*[gender]*/ = PhotonNetwork.Instantiate(GameSettings2.MALE_MODEL_PATH + GameSettings2.maleModels[0],
                                                          spawnPoint.transform.position,
                                                          spawnPoint.transform.rotation, 0) as GameObject;
            localPlayerObject/*[gender]*/.name = _name;
        }
        else
        {
            localPlayerObject/*[gender]*/ = PhotonNetwork.Instantiate(GameSettings2.FEMALE_MODEL_PATH + GameSettings2.femaleModels[0],
                                            spawnPoint.transform.position, spawnPoint.transform.rotation, 0) as GameObject;
            localPlayerObject/*[gender]*/.name = _name;
        }
    }

    void OnApplicationQuit()
    {
        Disconnect();
    }

    private void SetCustomProperties(PhotonPlayer player, int gender, int hairIndex,
                                     int hairColor, int eyesIndex, int eyesColor,
                                     int position, int chest, int hands, int WeaponMain,
                                     int Legs, int Boots, int WeaponOff)
    {
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {"gender", gender } , {"hairIndex", hairIndex },
            {"hairColor", hairColor } , {"eyesIndex", eyesIndex },
            {"eyesColor", eyesColor}, {"spawn", position },
            {"chest", chest}, {"hands", hands },
            {"WeaponMain", WeaponMain}, {"Legs", Legs },
            {"Boots", Boots}, {"WeaponOff", WeaponOff },
        };
        player.SetCustomProperties(customProperties);
    }
}
