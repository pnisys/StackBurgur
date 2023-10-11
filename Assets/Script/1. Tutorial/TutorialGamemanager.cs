using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Oculus.Interaction.HandGrab;

public class TutorialGamemanager : MonoBehaviour
{
    private void Update()
    {
        if (lgrabstatus.IsGrabbing == true && islgrab == false)
        {
            StartCoroutine(LHaticControl(0.1f));
            islgrab = true;
        }
        if (rgrabstatus.IsGrabbing == true && isrgrab == false)
        {
            StartCoroutine(RHaticControl(0.1f));
            isrgrab = true;
        }
        if (lgrabstatus.IsGrabbing == false)
        {
            islgrab = false;
        }
        if (rgrabstatus.IsGrabbing == false)
        {
            isrgrab = false;
        }
    }
    IEnumerator LHaticControl(float delay)
    {
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(delay);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
    }
    IEnumerator RHaticControl(float delay)
    {
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(delay);
        OVRInput.SetControllerVibration(0, 0f, OVRInput.Controller.RTouch);
    }
    //1. ������ �Ŵ� �տ� ����
    public bool isClerk = false;
    //2. ������ �����ϴ� ��
    public bool isThinking = false;
    //3. �ֹ� ����
    public bool isOrder = false;
    //4. ������ ���� ����
    public bool isSelect = false;

    public bool iscompletesuccess = false;
    public bool islittlesuccess = false;
    public bool isfail = false;

    public bool isTray = false;
    bool islgrab = false;
    bool isrgrab = false;

    public GameObject[] people;
    public Animator animator;
    public HandGrabInteractor lgrabstatus;
    public HandGrabInteractor rgrabstatus;
    public int score = 0;
    public int lifescore = 5;

    //���� ǳ�� �����ִ� �ð�
    public float orderlimitTime = 15;
    //�ֹ� �ð� 30��
    public float limitTime = 90;

    private void Start()
    {
        //gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.bgmSound;
    }
}
