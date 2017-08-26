using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float speed;
    Vector3 move;

    // Update is called once per frame
    void Update()
    {
        float x =  Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        move.x = x * speed;
        move.z = y * speed;

        transform.Translate(move * Time.deltaTime);
    }
}
