using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class FisrtChoice : MonoBehaviour
{
    public GameObject canvas;
    public AudioSource audiosource;
    public AudioClip audioclip;
    public AudioSource bgmsource;
    public GameObject keyboard;
    public GameObject avatarselect;
    public GameObject logoCanvas;


    public void ClickSound()
    {
        audiosource.PlayOneShot(audioclip);
    }

    public void TutorialStart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameStart()
    {
        canvas.SetActive(false);
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        logoCanvas.SetActive(false);
        avatarselect.SetActive(true);
        //keyboard.SetActive(true);
    }

    public void GameStartEnter(string a)
    {
        SoundManager.instance.username = a;
        keyboard.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void Setting()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(true);

        SoundManager.instance.bgmSound = canvas.transform.GetChild(3).GetChild(2).GetComponent<Slider>().value;
        SoundManager.instance.anothersound = canvas.transform.GetChild(3).GetChild(3).GetComponent<Slider>().value;
    }
    public void BgmSoundControl()
    {
        SoundManager.instance.bgmSound = canvas.transform.GetChild(3).GetChild(2).GetComponent<Slider>().value;
        if (SoundManager.instance.bgmSound <= 0.8)
        {
            bgmsource.volume = SoundManager.instance.bgmSound;
        }
    }
    public void AnotherSound()
    {
        SoundManager.instance.anothersound = canvas.transform.GetChild(3).GetChild(3).GetComponent<Slider>().value;
        audiosource.volume = SoundManager.instance.anothersound;
    }
    public void Previous()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void PreviousKeyboard()
    {
        keyboard.SetActive(false);
        avatarselect.SetActive(true);
    }

    public void PreviousAvatar()
    {
        avatarselect.SetActive(false);
        logoCanvas.SetActive(true);
        canvas.SetActive(true);
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void KeyboardEnter()
    {
        SoundManager.instance.username = keyboard.transform.GetChild(1).GetComponent<InputField>().text;
        keyboard.SetActive(false);
        avatarselect.SetActive(true);
    }

    public void AvatarEnter()
    {
        avatarselect.SetActive(false);
        keyboard.SetActive(true);
    }

    private void Start()
    {
        RankAvatarManager rankAvatarManager = SoundManager.instance.gameObject.GetComponent<RankAvatarManager>();
        //버튼 달아주기
        keyboard.transform.GetChild(2).GetChild(2).GetChild(12).GetComponent<Button>().onClick.AddListener(SoundManager.instance.gameObject.GetComponent<RankAvatarManager>()._PushCheck);
        //씬 한번 전환되서 온것일때
        if (SoundManager.instance.scenenext != 0)
        {
            avatarselect.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(rankAvatarManager._PrevFaceNum);
            avatarselect.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(rankAvatarManager._NextFaceNum);
            avatarselect.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(rankAvatarManager._PrevHairNum);
            avatarselect.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(rankAvatarManager._NextHairNum);
            avatarselect.transform.GetChild(10).GetComponent<Button>().onClick.AddListener(rankAvatarManager._PrevEyeNum);
            avatarselect.transform.GetChild(11).GetComponent<Button>().onClick.AddListener(rankAvatarManager._NextEyeNum);
            avatarselect.transform.GetChild(13).GetComponent<Button>().onClick.AddListener(rankAvatarManager._PrevAccNum);
            avatarselect.transform.GetChild(14).GetComponent<Button>().onClick.AddListener(rankAvatarManager._NextAccNum);

            rankAvatarManager.LegacyText = keyboard.transform.GetChild(3).GetComponent<Text>();
            rankAvatarManager.manImg = avatarselect.transform.GetChild(1).GetComponent<Image>();
            rankAvatarManager.manFaceImg = avatarselect.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            rankAvatarManager.manFaceText = avatarselect.transform.GetChild(3).GetComponent<Text>();
            rankAvatarManager.manHairImg = avatarselect.transform.GetChild(1).GetChild(2).GetComponent<Image>();
            rankAvatarManager.manBackHairImg = avatarselect.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            rankAvatarManager.manHairText = avatarselect.transform.GetChild(6).GetComponent<Text>();
            rankAvatarManager.manEyeImg = avatarselect.transform.GetChild(1).GetChild(3).GetComponent<Image>();
            rankAvatarManager.manEyeText = avatarselect.transform.GetChild(9).GetComponent<Text>();
            rankAvatarManager.manAccImg = avatarselect.transform.GetChild(1).GetChild(4).GetComponent<Image>();
            rankAvatarManager.manAccText = avatarselect.transform.GetChild(12).GetComponent<Text>();
            rankAvatarManager.nameInput = keyboard.transform.GetChild(1).GetComponent<InputField>();

            rankAvatarManager.manFacesNum = Random.Range(1, 7);
            rankAvatarManager.manHairNum = Random.Range(1, 16);
            rankAvatarManager.manEyesNum = Random.Range(1, 7);
            rankAvatarManager.manAccsNum = Random.Range(1, 13);

            rankAvatarManager.StartCoroutine(rankAvatarManager.ManFaceSystem());
            rankAvatarManager.StartCoroutine(rankAvatarManager.ManHairSystem());
            rankAvatarManager.StartCoroutine(rankAvatarManager.ManEyeSystem());
            rankAvatarManager.StartCoroutine(rankAvatarManager.ManAccSystem());


        }

      




    }
}
