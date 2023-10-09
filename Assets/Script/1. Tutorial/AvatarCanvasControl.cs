using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarCanvasControl : MonoBehaviour
{

    RankAvatarManager rankavatarmanager;

    public Button leftarrowbutton_3;
    public Button reftarrowbutton_3;

    public Button leftarrowbutton;
    public Button reftarrowbutton;

    public Button leftarrowbutton_2;
    public Button reftarrowbutton_2;

    public Button leftarrowbutton_1;
    public Button reftarrowbutton_1;

    void Start()
    {
        leftarrowbutton_3.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._PrevFaceNum);
        reftarrowbutton_3.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._NextFaceNum);
        leftarrowbutton.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._PrevHairNum);
        reftarrowbutton.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._NextHairNum);
        leftarrowbutton_2.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._PrevEyeNum);
        reftarrowbutton_2.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._NextEyeNum);
        leftarrowbutton_1.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._PrevAccNum);
        reftarrowbutton_1.onClick.AddListener(SoundManager.instance.GetComponent<RankAvatarManager>()._NextAccNum);
    }

   
}
