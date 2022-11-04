using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    GameObject centereye;
    private void Start()
    {
        centereye = GameObject.FindGameObjectWithTag("OVRCAMERA");
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(centereye.transform);
    }
}
