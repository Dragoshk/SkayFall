using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //faz o objeto ficar olhando para a camera
        transform.LookAt(Camera.main.transform);
    }
}
