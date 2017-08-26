using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkLocalController : PunBehaviour
{
    [SerializeField]
    Behaviour[] ComponetsToDisable;
    GameObject[] Enemies;
    GameObject[] Allies;
    public int Team;

    public DataWorker DW;
    public GameObject localPlayerObject;
    private int assignedUserId;

    //Itens
    ItemDatabase database;
    GameObject item0;
    GameObject item1;
    GameObject item2;
    GameObject item3;
    GameObject item4;
    GameObject item5;
    // Use this for initialization
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        DW = GameObject.FindGameObjectWithTag("DataWorker").GetComponent<DataWorker>();

        //Character
        int gender = DW._gender;
        int _hair = DW._hair;
        int _eyes = 1;
        int _hairColor = (int)photonView.owner.CustomProperties["hairColor"];
        int _eyesColor = (int)photonView.owner.CustomProperties["eyesColor"];
        //itens
        int chest = (int)photonView.owner.CustomProperties["chest"];
        int hands = (int)photonView.owner.CustomProperties["hands"];
        int WeaponMain = (int)photonView.owner.CustomProperties["WeaponMain"];
        int Legs = (int)photonView.owner.CustomProperties["Legs"];
        int Boots = (int)photonView.owner.CustomProperties["Boots"];
        int WeaponOff = (int)photonView.owner.CustomProperties["WeaponOff"];

        //Debug.Log("uq ta na rede:" + (int)photonView.owner.CustomProperties["hairIndex"] + "uq deveria ser:" +DW._hair);

        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 10;

        if (!photonView.isMine)
        {
            for (int i = 0; i < ComponetsToDisable.Length; i++)
            {
                ComponetsToDisable[i].enabled = false;
            }
        }

        photonView.RPC("Properties", PhotonTargets.AllBufferedViaServer,
                       gender, _hair, _hairColor, _eyes, _eyesColor);

        photonView.RPC("AddItem", PhotonTargets.AllBufferedViaServer,
                       gender, chest, hands, WeaponMain, Legs, Boots, WeaponOff);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {

        }
        else
        {

        }
    }

    [PunRPC]
    public void Properties(int gender, int hairIndex, int hairColor, int eyesIndex, int eyesColor)
    {
        if (gender == 0)
        {
            GameObject hairStyle;
            int hairMechIndex = hairIndex;

            hairStyle = Instantiate(Resources.Load(GameSettings2.MALE_HAIR_PATH + "Hair" + " " + hairMechIndex),
                                photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.position,
                                photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.rotation) as GameObject;

            hairStyle.transform.parent = photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform;
            hairStyle.transform.localPosition = Vector3.zero;
            hairStyle.transform.rotation = new Quaternion(0, 0, 0, 0);

            Texture temp = Resources.Load(GameSettings2.HAIR_TEXURE_PATH + ((HairColorNames)hairColor).ToString()) as Texture;
            photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp;

            GameObject eyesStyle;
            int eyesMechIndex = eyesIndex;

            eyesStyle = Instantiate(Resources.Load(GameSettings2.MALE_EYES_PATH + "Eyes" + " " + eyesMechIndex),
                                photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.position,
                                photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.rotation) as GameObject;

            eyesStyle.transform.parent = photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform;
            eyesStyle.transform.localPosition = Vector3.zero;
            eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);

            Texture temp2 = Resources.Load(GameSettings2.EYES_TEXURE_PATH + ((EyesColorNames)eyesColor).ToString()) as Texture;
            photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp2;

            if (photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.childCount != 1 &&
                photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.childCount != 1)
            {

                for (int cnt = 1; cnt < photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.childCount; cnt++)
                    Destroy(photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.GetChild(cnt).gameObject);

                for (int cnt = 1; cnt < photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.childCount; cnt++)
                    Destroy(photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.GetChild(cnt).gameObject);
            }
        }
        else
        {
            GameObject hairStyle;
            int hairMechIndex = hairIndex;

            hairStyle = Instantiate(Resources.Load(GameSettings2.FEMALE_HAIR_PATH + "Hair" + " " + hairMechIndex),
                        photonView.GetComponent<PlayerCharacter>().HairAnchor.transform.position,
                        photonView.GetComponent<PlayerCharacter>().HairAnchor.transform.rotation) as GameObject;

            hairStyle.transform.parent = photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform;
            hairStyle.transform.localPosition = Vector3.zero;
            hairStyle.transform.rotation = new Quaternion(0, 0, 0, 0);

            Texture temp = Resources.Load(GameSettings2.HAIR_TEXURE_PATH + ((HairColorNames)hairColor).ToString()) as Texture;
            photonView.GetComponent<PlayerCharacter>().HairAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp;

            GameObject eyesStyle;
            int eyesMechIndex = eyesIndex;

            eyesStyle = Instantiate(Resources.Load(GameSettings2.FEMALE_EYES_PATH + "Eyes" + " " + eyesMechIndex),
                        photonView.GetComponent<PlayerCharacter>().EyesAnchor.transform.position,
                        photonView.GetComponent<PlayerCharacter>().EyesAnchor.transform.rotation) as GameObject;

            eyesStyle.transform.parent = photonView.GetComponent<PlayerCharacter>().EyesAnchor.transform;
            eyesStyle.transform.localPosition = Vector3.zero;
            eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);

            Texture temp2 = Resources.Load(GameSettings2.EYES_TEXURE_PATH + ((EyesColorNames)eyesColor).ToString()) as Texture;
            photonView.GetComponent<PlayerCharacter>().EyesAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp2;

            if (photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.childCount != 1 &&
                photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.childCount != 1)
            {
                for (int cnt = 1; cnt < photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.childCount; cnt++)
                    Destroy(photonView.transform.GetComponent<PlayerCharacter>().HairAnchor.transform.GetChild(cnt).gameObject);

                for (int cnt = 1; cnt < photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.childCount; cnt++)
                    Destroy(photonView.transform.GetComponent<PlayerCharacter>().EyesAnchor.transform.GetChild(cnt).gameObject);
            }
        }
    }

    [PunRPC]
    void AddItem(int gender, int chest, int hands, int WeaponMain, int Legs, int Boots, int WeaponOff)
    {
        if (database.items[chest].itemType == Item.ItemType.Chest)
        {
            if (gender == 0)
            {
                //item0 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[chest].itemName),
                //                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.position,
                //                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.rotation)
                //                    as GameObject;
                //Debug.Log("Itens");

                //if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.childCount != 1)
                //{
                //    for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.childCount; cnt++)
                //        Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.GetChild(cnt).gameObject);
                //}
                //item0.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform;
            }
            else
            {
                item0 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[chest].itemName),
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.position,
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.rotation)
                                    as GameObject;
                item0.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform;

                if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.childCount != 1)
                {
                    for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.childCount; cnt++)
                        Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.GetChild(cnt).gameObject);
                }
            }
        }

        if (database.items[hands].itemType == Item.ItemType.Hands)
        {
            //if (gender == 0)
            //{
            //    item1 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[hands].itemName),
            //                        photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.position,
            //                        photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.rotation)
            //                        as GameObject;
            //    item1.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.transform;
            //}
            //else
            //{
            //    item1 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[hands].itemName),
            //                        photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.position,
            //                        photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.rotation)
            //                        as GameObject;
            //    item1.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform;
            //}
        }

        if (database.items[WeaponMain].itemType == Item.ItemType.Weapon)
        {
            if (typeof(Weapon) == database.items[WeaponMain].GetType())
            {
                Weapon wep = (Weapon)database.items[WeaponMain];

                if (wep.weaponType == Weapon.WeaponType.MainHand)
                {
                    item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + database.items[WeaponMain].itemName),
                                     photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position,
                                     photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation)
                                     as GameObject;

                    item2.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform;
                    item2.transform.position = photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position;
                    item2.transform.rotation = photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation;

                    if (photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.childCount != 1)
                    {
                        for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.childCount; cnt++)
                            Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.GetChild(cnt).gameObject);
                    }
                }

                if (wep.weaponType == Weapon.WeaponType.TwoHand)
                {
                    photonView.GetComponent<Player_Controler>().WeaponT2h = true;

                    item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + database.items[WeaponMain].itemName),
                                            photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.position,
                                            photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.rotation)
                                            as GameObject;

                    item2.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform;
                    item2.transform.position = photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.position;
                    item2.transform.rotation = photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.rotation;


                    if (photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.childCount != 1)
                    {
                        for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.childCount; cnt++)
                            Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.GetChild(cnt).gameObject);
                    }
                }
            }
        }

        if (database.items[Legs].itemType == Item.ItemType.Legs)
        {
            if (gender == 0)
            {
                //item4 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[Legs].itemName),
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position,
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation)
                //                    as GameObject;

                //item4.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform;

                //if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.childCount != 1)
                //{
                //    for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.childCount; cnt++)
                //        Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.GetChild(cnt).gameObject);
                //}
            }
            else
            {
                item4 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[Legs].itemName),
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position,
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation)
                                    as GameObject;

                item4.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform;
                item4.transform.position = photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position;
                item4.transform.rotation = photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation;

                if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.childCount != 1)
                {
                    for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.childCount; cnt++)
                        Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.GetChild(cnt).gameObject);
                }
            }
        }

        if (database.items[Boots].itemType == Item.ItemType.Boots)
        {
            if (gender == 0)
            {
                //item3 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[Boots].itemName),
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position,
                //                   slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation)
                //                    as GameObject;

                //item3.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform;
                //item3.transform.position = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position;
                //item3.transform.rotation = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation;

                //if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.childCount != 1)
                //{
                //    for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.childCount; cnt++)
                //        Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.GetChild(cnt).gameObject);
                //}
            }
            else
            {
                item3 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[Boots].itemName),
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position,
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation)
                                    as GameObject;

                item3.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform;
                item3.transform.position = photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position;
                item3.transform.rotation = photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation;

                if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.childCount != 1)
                {
                    for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.childCount; cnt++)
                        Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.GetChild(cnt).gameObject);
                }
            }
        }

        if (database.items[WeaponOff].itemType == Item.ItemType.WeaponOff)
        {
            item5 = Instantiate(Resources.Load(GameSettings2.MELLE_SHILD_MESH_PATH + database.items[WeaponOff].itemName),
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.position,
                                    photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.rotation)
                                    as GameObject;

            item5.transform.parent = photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform;
            item5.transform.position = photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.position;
            item5.transform.rotation = photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.rotation;

            if (photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.childCount != 1)
            {
                for (int cnt = 1; cnt < photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.childCount; cnt++)
                    Destroy(photonView.transform.GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.GetChild(cnt).gameObject);
            }
        }
    }
}
