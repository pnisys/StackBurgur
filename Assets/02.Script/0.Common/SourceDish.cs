using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

public class SourceDish : MonoBehaviour
{
    public bool sourcebottleentry = true;
    public HandGrabInteractor grabstatus;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SOURCE"))
        {
            sourcebottleentry = false;
            StartCoroutine(CheckingSource(other));
        }
    }

    IEnumerator CheckingSource(Collider other)
    {
        while (true)
        {
            yield return null;
            if (grabstatus.IsGrabbing == false && other.gameObject.GetComponent<SourceControl>().isEntry == false)
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.transform.localPosition = new Vector3(0, 0, 0);
                other.transform.localRotation = Quaternion.Euler(-90, 0, -90);
                yield break;
            }
            else if (grabstatus.IsGrabbing == false && other.gameObject.GetComponent<SourceControl>().isEntry == true)
            {
                yield break;
            }
        }
    }

}
