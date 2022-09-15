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
    public GameObject keyboard;
    public AudioSource audiosource;
    public AudioClip audioclip;
    public AudioSource bgmsource;


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
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        //SceneManager.LoadScene(2);
        keyboard.SetActive(true);
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
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void Start()
    {
        SoundManager.instance.username = "�踻��";
        SceneManager.LoadScene(2);
    }
}
