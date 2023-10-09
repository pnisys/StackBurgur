using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
public class TestTrayControl : MonoBehaviour
{
    //���� �ɶ�

    public HandGrabInteractor grabstatus;


    //�׳� �̺�Ʈ�� ȣ���ұ�?

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
        print("����");
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

    //1. ������ ���´�.
    //2. �׶����� �˻��ؼ� ���� ���´�.
    private IEnumerator OnTriggerEnter(Collider other)
    {
        //Food������ ������
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
    //    //������ ������
    //    if(other.gameObject.layer==LayerMask.NameToLayer("FOOD"))
    //    {
    //    }
    //}


}
