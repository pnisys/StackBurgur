using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayserPointer : MonoBehaviour
{
    LineRenderer layser;
    RaycastHit coliied_object;
    public float raycastDistance = 100f;
    // Start is called before the first frame update
    void Start()
    {
        layser = gameObject.AddComponent<LineRenderer>();
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        layser.material = material;

        layser.positionCount = 2;

        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        layser.SetPosition(0, transform.position);
        layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));

        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);

        if (Physics.Raycast(transform.position, transform.forward, out coliied_object, raycastDistance))
        {
            layser.SetPosition(1, coliied_object.point);
        }
        else
        {
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));
        }
    }
}
