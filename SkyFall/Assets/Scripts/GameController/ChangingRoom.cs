using UnityEngine;
using System.Collections;
using System;

public class ChangingRoom : MonoBehaviour
{
    private CharacterAssets ca;
    private PlayerCharacter pc;

    private int _charModelIndex = 0;
    private int _weaponIndex = 0;
    private int _hairIndex = 0;

    private string _charModelName = "LOL";
    private GameObject _characterMesh;

    // Use this for initialization
    void Start ()
    {
        ca = GameObject.Find("Character Asset Maneger").GetComponent<CharacterAssets>();

        InstantiateCharaterModel();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnGUI()
    {
        ChanageCharacterMeshGUI();
        ChanageWeaponMeshGUI();
        ChanageHairMeshGUI();
        RotateCharacterModel();
    }

    private void RotateCharacterModel()
    {
        if (GUI.RepeatButton(new Rect(Screen.width * .5f - 95, Screen.height * .5f, 30, 30), "<"))
            _characterMesh.transform.Rotate(Vector3.up * Time.deltaTime * 100);

        if (GUI.RepeatButton(new Rect(Screen.width * .5f + 65, Screen.height * .5f, 30, 30), ">"))
            _characterMesh.transform.Rotate(Vector3.up * Time.deltaTime * -100);
    }

    private void ChanageCharacterMeshGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 35, 120, 30), _charModelName.ToString()))
        {
            _charModelIndex++;
            InstantiateCharaterModel();
            Debug.Log(_charModelName);
        }
    }

    private void ChanageWeaponMeshGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 70, 120, 30), _weaponIndex.ToString()))
        {
            //_weaponIndex++;
            //InstantiateWeaponModel();
            Debug.Log(_weaponIndex);
        }
    }

    private void ChanageHairMeshGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 105, 120, 30), _hairIndex.ToString()))
        {
            _hairIndex++;
            InstantiateHairModel();
            Debug.Log(_hairIndex);
        }
    }

    private void InstantiateWeaponModel()
    {
        if (_weaponIndex > ca.weaponMesh.Length - 1)
            _weaponIndex = 0;

        if (pc.wm_R.transform.childCount > 0)
            for (int cnt = 0; cnt < pc.wm_R.transform.childCount; cnt++)
                Destroy(transform.GetChild(cnt).gameObject);

        GameObject wR = Instantiate(ca.weaponMesh[_weaponIndex], pc.wm_R.transform.position, Quaternion.identity) as GameObject;
        wR.transform.parent = pc.wm_R.transform;
        wR.transform.rotation = new Quaternion(0, 0, 0, 0);

        /*GameObject wL = Instantiate(ca.weaponMesh[_weaponIndex], pc.Weapon_Holder_L.transform.position, Quaternion.identity) as GameObject;
        wL.transform.parent = pc.Weapon_Holder_L.transform;
        wL.transform.rotation = new Quaternion(0, 0, 0, 0);*/
    }

    private void InstantiateHairModel()
    {
        if (_hairIndex > ca.hairMesh.Length - 1)
            _hairIndex = 0;

        if (pc.head.transform.childCount > 0)
            for (int cnt = 0; cnt < pc.head.transform.childCount; cnt++)
                Destroy(pc.head.transform.GetChild(cnt).gameObject);

        GameObject H = Instantiate(ca.hairMesh[_hairIndex], pc.head.transform.position, Quaternion.identity) as GameObject;
        H.transform.parent = pc.head.transform;
        H.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void InstantiateCharaterModel()
    {
        switch (_charModelIndex)
        {
            case 1:
                _charModelName = "Female";
                break;
            default:
                _charModelIndex = 0;
                _charModelName = "Male";
                break;
        }

        Quaternion oldRot;

        if (_characterMesh == null)
            oldRot = transform.rotation;
        else
            oldRot = _characterMesh.transform.rotation;

        if (transform.childCount != 0)
            for (int cnt = 0; cnt < transform.childCount; cnt++)
                Destroy(transform.GetChild(cnt).gameObject);

        _characterMesh = Instantiate(ca.characterMesh[_charModelIndex], transform.position, Quaternion.identity) as GameObject;

        _characterMesh.transform.parent = transform;
        _characterMesh.transform.rotation = oldRot;

        pc = _characterMesh.GetComponent<PlayerCharacter>();
        //InstantiateWeaponModel();
        InstantiateHairModel();
    }
}
