using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FisrtChoice : MonoBehaviour
{
    public GameObject canvas;
    public GameObject keyboard;
    public void TutorialStart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameStart()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        keyboard.SetActive(true);
        //SceneManager.LoadScene(2);
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
    }
    public void AnotherSound()
    {
        SoundManager.instance.anothersound = canvas.transform.GetChild(3).GetChild(3).GetComponent<Slider>().value;
    }
    public void Previous()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
    }
}
