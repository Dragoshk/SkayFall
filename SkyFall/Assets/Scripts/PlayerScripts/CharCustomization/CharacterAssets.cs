using UnityEngine;
using System.Collections;

public class CharacterAssets : MonoBehaviour
{
    public GameObject[] characterMesh;
    public GameObject[] weaponMesh;
    public GameObject[] hairMesh;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
