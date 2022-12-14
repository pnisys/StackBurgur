using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

public class SourceDish : MonoBehaviour
{

    GameObject[] hands;
    HandGrabInteractor[] grabstatus = new HandGrabInteractor[2];
    public bool sourcebottleentry = true;
    public bool islgrabstatus = false;
    public bool isrgrabstatus = false;

    private void Start()
    {
        hands = GameObject.FindGameObjectsWithTag("HANDGRAB");
        for (int i = 0; i < 2; i++)
        {
            grabstatus[i] = hands[i].GetComponent<HandGrabInteractor>();
            print(grabstatus[i]);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SOURCE"))
        {
            sourcebottleentry = false;
            StartCoroutine(CheckingSource(other));
        }
    }
    IEnumerator HaticControl(float delay)
    {
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(delay);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0f, OVRInput.Controller.RTouch);
    }
    IEnumerator CheckingSource(Collider other)
    {
        while (true)
        {
            yield return null;
            if (grabstatus[0].IsGrabbing == true && islgrabstatus == false)
            {
                StartCoroutine(HaticControl(0.2f));
                islgrabstatus = true;
            }
            else if (grabstatus[1].IsGrabbing == true && isrgrabstatus == false)
            {
                StartCoroutine(HaticControl(0.2f));
                isrgrabstatus = true;
            }
            if (((grabstatus[0].IsGrabbing == false && islgrabstatus == true) || (grabstatus[1].IsGrabbing == false && isrgrabstatus == true)) && other.gameObject.GetComponent<SourceControl>().isEntry == false)
            {
                islgrabstatus = false;
                isrgrabstatus = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.transform.localPosition = new Vector3(0, 0, 0);
                other.transform.localRotation = Quaternion.Euler(-90, 0, -90);
                yield break;
            }
            else if (((grabstatus[0].IsGrabbing == false && islgrabstatus == true) || (grabstatus[1].IsGrabbing == false && isrgrabstatus == true)) && other.gameObject.GetComponent<SourceControl>().isEntry == true)
            {
                islgrabstatus = false;
                isrgrabstatus = false;
                yield break;
            }
        }
    }

}
