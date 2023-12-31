using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCanvasControl : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip[] audioclip;
    public GameObject stageCanvas;
    public Sprite[] stagesprite;
    public GameManager gamemanager;
    private void OnEnable()
    {
        PeopleAnimator.OnStageChange += this.StageUp;
        PeopleAnimator.OnLifeDie += this.Die;
        GameManager.OnButtonLifeDie += this.Die;
        PeopleAnimator.OnClear += this.Clear;
    }

    private void OnDisable()
    {
        PeopleAnimator.OnStageChange -= this.StageUp;
        GameManager.OnButtonLifeDie -= this.Die;
        PeopleAnimator.OnLifeDie -= this.Die;
        PeopleAnimator.OnClear -= this.Clear;
    }
    public void Clear()
    {
        audiosource.PlayOneShot(audioclip[2]);
        stageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = stagesprite[5];
        stageCanvas.SetActive(true);
    }
    public void StageUp()
    {
        StartCoroutine(RealStageUp());
    }

    public void Die()
    {
        audiosource.PlayOneShot(audioclip[1]);
        stageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = stagesprite[6];
        stageCanvas.SetActive(true);
    }
    IEnumerator Start()
    {
        audiosource.PlayOneShot(audioclip[0]);
        yield return new WaitForSeconds(1.5f);
        stageCanvas.SetActive(false);
        gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
    }

    IEnumerator RealStageUp()
    {
        if (gamemanager.stage == 1)
        {
            yield return new WaitForSeconds(2.5f);
            gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
            stageCanvas.SetActive(false);
        }
        else if (gamemanager.stage == 2)
        {
            stageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = stagesprite[1];
            stageCanvas.SetActive(true);
            audiosource.PlayOneShot(audioclip[0]);
            yield return new WaitForSeconds(2.5f);
            //이제 다른 사람 한명을 켜야됨
            gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
            stageCanvas.SetActive(false);
        }
        else if (gamemanager.stage == 3)
        {
            stageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = stagesprite[2];
            stageCanvas.SetActive(true);
            audiosource.PlayOneShot(audioclip[0]);
            yield return new WaitForSeconds(2.5f);
            //이제 다른 사람 한명을 켜야됨
            gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
            stageCanvas.SetActive(false);
        }
        else if (gamemanager.stage == 4)
        {
            stageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = stagesprite[3];
            stageCanvas.SetActive(true);
            audiosource.PlayOneShot(audioclip[0]);
            yield return new WaitForSeconds(2.5f);
            //이제 다른 사람 한명을 켜야됨
            gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
            stageCanvas.SetActive(false);
        }
        else if (gamemanager.stage == 5)
        {
            stageCanvas.transform.GetChild(0).GetComponent<Image>().sprite = stagesprite[4];
            stageCanvas.SetActive(true);
            audiosource.PlayOneShot(audioclip[0]);
            yield return new WaitForSeconds(2.5f);
            //이제 다른 사람 한명을 켜야됨
            gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
            stageCanvas.SetActive(false);
        }
    }
}
