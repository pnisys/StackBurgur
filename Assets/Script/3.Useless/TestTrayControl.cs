using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
public class TestTrayControl : MonoBehaviour
{
    //적재 될때

    public HandGrabInteractor grabstatus;


    //그냥 이벤트로 호출할까?

    public delegate void Traying(Collider other);
    public static event Traying OnTraying;

    public Transform trayPosition;

    private void OnEnable()
    {
        TestTrayControl.OnTraying += this.AAA;
    }
    private void OnDisable()
    {
        TestTrayControl.OnTraying -= this.AAA;
    }

    void AAA(Collider other)
    {
        StartCoroutine(BBB(other));
    }

    IEnumerator BBB(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        print("들어옴");
        other.transform.parent = trayPosition;
        other.transform.localPosition = new Vector3(0, 0, 0);
        other.transform.localRotation = Quaternion.identity;
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        //while (true)
        //{
        //    yield return null;
        //    if (other.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        //    {
        //        other.gameObject.GetComponent<Rigidbody>().mass = 10;
        //        break;
        //    }
        //}
    }

    //1. 음식이 들어온다.
    //2. 그때부터 검사해서 손을 놓는다.
    private IEnumerator OnTriggerEnter(Collider other)
    {
        //Food음식이 들어오면
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            while (grabstatus.IsGrabbing == false)
            {
                yield return null;
                OnTraying(other);
                yield break;
            }
        }
    }

    //private IEnumerator OnTriggerExit(Collider other)
    //{
    //    //음식이 나가면
    //    if(other.gameObject.layer==LayerMask.NameToLayer("FOOD"))
    //    {
    //    }
    //}


}
