using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RankAvatarManager : MonoBehaviour
{
    public string avatarAlpha;

    [Space(10)]
    [Header("Avatar Image Element")]
    public Image manImg;
    int randomFace;
    int randomHair;
    int randomEye;
    int randomAcce;

    [Space(10)]
    [Header("Face Image Element")]
    public Image manFaceImg;
    public Text manFaceText;
    public Sprite[] manFaces;
    public int manFacesNum;
    public string faceAlpha;

    [Space(10)]
    [Header("Hair Image Element")]
    public Image manHairImg;
    public Image manBackHairImg;
    public Text manHairText;
    public Sprite[] manHairs;
    public Sprite[] manBackHairs;
    public int manHairNum;
    public string hairAlpha;

    [Space(10)]
    [Header("Eye Image Element")]
    public Image manEyeImg;
    public Text manEyeText;
    public Sprite[] manEyes;
    public int manEyesNum;
    public string eyeAlpha;

    [Space(10)]
    [Header("Accessory Image Element")]
    public Image manAccImg;
    public Text manAccText;
    public Sprite[] manAccs;
    public int manAccsNum;
    public string acceAlpha;

    bool isRunning = true;

    IEnumerator faceCoroutine;
    IEnumerator hairCoroutine;
    IEnumerator eyeCoroutine;
    IEnumerator accCoroutine;

    void Start()
    {
        faceCoroutine = ManFaceSystem();
        hairCoroutine = ManHairSystem();
        eyeCoroutine = ManEyeSystem();
        accCoroutine = ManAccSystem();

        randomFace = Random.Range(1, 7);
        randomHair = Random.Range(1, 16);
        randomEye = Random.Range(1, 7);
        randomAcce = Random.Range(1, 13);

        manFacesNum = randomFace;
        manHairNum = randomHair;
        manEyesNum = randomEye;
        manAccsNum = randomAcce;

        StartCoroutine(faceCoroutine);
        StartCoroutine(hairCoroutine);
        StartCoroutine(eyeCoroutine);
        StartCoroutine(accCoroutine);
    }
    // 아바타 상태 확인
    IEnumerator ManFaceSystem()
    {
        while (isRunning)
        {
            switch (manFacesNum)
            {
                case 1:
                    manFaceImg.sprite = manFaces[0];
                    manFaceText.text = $"피부/얼굴형{manFacesNum}";
                    faceAlpha = "a";
                    break;
                case 2:
                    manFaceImg.sprite = manFaces[1];
                    manFaceText.text = $"피부/얼굴형{manFacesNum}";
                    faceAlpha = "b";
                    break;
                case 3:
                    manFaceImg.sprite = manFaces[2];
                    manFaceText.text = $"피부/얼굴형{manFacesNum}";
                    faceAlpha = "c";
                    break;
                case 4:
                    manFaceImg.sprite = manFaces[3];
                    manFaceText.text = $"피부/얼굴형{manFacesNum}";
                    faceAlpha = "d";
                    break;
                case 5:
                    manFaceImg.sprite = manFaces[4];
                    manFaceText.text = $"피부/얼굴형{manFacesNum}";
                    faceAlpha = "e";
                    break;
                case 6:
                    manFaceImg.sprite = manFaces[5];
                    manFaceText.text = $"피부/얼굴형{manFacesNum}";
                    faceAlpha = "f";
                    break;
            }

            yield return new WaitForSeconds(0);
        }
    }
    IEnumerator ManHairSystem()
    {
        while (isRunning)
        {
            switch (manHairNum)
            {
                case 1:
                    manBackHairImg.gameObject.SetActive(false);
                    manHairImg.gameObject.SetActive(true);
                    manHairImg.sprite = manHairs[0];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "a";
                    break;
                case 2:
                    manHairImg.sprite = manHairs[1];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "b";
                    break;
                case 3:
                    manHairImg.sprite = manHairs[2];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "c";
                    break;
                case 4:
                    manHairImg.sprite = manHairs[3];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "d";
                    break;
                case 5:
                    manHairImg.sprite = manHairs[4];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "e";
                    break;
                case 6:
                    manHairImg.sprite = manHairs[5];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "f";
                    break;
                case 7:
                    manHairImg.sprite = manHairs[6];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "g";
                    break;
                case 8:
                    manHairImg.sprite = manHairs[7];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "h";
                    break;
                case 9:
                    manHairImg.sprite = manHairs[8];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "i";
                    break;
                case 10:
                    manHairImg.sprite = manHairs[9];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "j";
                    break;
                case 11:
                    manHairImg.sprite = manHairs[10];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "k";
                    break;
                case 12:
                    manBackHairImg.gameObject.SetActive(false);
                    manHairImg.gameObject.SetActive(true);
                    manHairImg.sprite = manHairs[11];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "l";
                    break;

                case 13:
                    manHairImg.gameObject.SetActive(false);
                    manBackHairImg.gameObject.SetActive(true);
                    manBackHairImg.sprite = manBackHairs[0];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "m";
                    break;
                case 14:
                    manBackHairImg.sprite = manBackHairs[1];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "n";
                    break;
                case 15:
                    manHairImg.gameObject.SetActive(false);
                    manBackHairImg.gameObject.SetActive(true);
                    manBackHairImg.sprite = manBackHairs[2];
                    manHairText.text = $"머리{manHairNum}";
                    hairAlpha = "o";
                    break;
            }

            yield return new WaitForSeconds(0);
        }
    }
    IEnumerator ManEyeSystem()
    {
        while (isRunning)
        {
            switch (manEyesNum)
            {
                case 1:
                    manEyeImg.sprite = manEyes[0];
                    manEyeText.text = $"표정{manEyesNum}";
                    eyeAlpha = "a";
                    break;
                case 2:
                    manEyeImg.sprite = manEyes[1];
                    manEyeText.text = $"표정{manEyesNum}";
                    eyeAlpha = "b";
                    break;
                case 3:
                    manEyeImg.sprite = manEyes[2];
                    manEyeText.text = $"표정{manEyesNum}";
                    eyeAlpha = "c";
                    break;
                case 4:
                    manEyeImg.sprite = manEyes[3];
                    manEyeText.text = $"표정{manEyesNum}";
                    eyeAlpha = "d";
                    break;
                case 5:
                    manEyeImg.sprite = manEyes[4];
                    manEyeText.text = $"표정{manEyesNum}";
                    eyeAlpha = "e";
                    break;
                case 6:
                    manEyeImg.sprite = manEyes[5];
                    manEyeText.text = $"표정{manEyesNum}";
                    eyeAlpha = "f";
                    break;
            }

            yield return new WaitForSeconds(0);
        }
    }
    IEnumerator ManAccSystem()
    {
        while (isRunning)
        {
            switch (manAccsNum)
            {
                case 1:
                    manAccImg.sprite = manAccs[0];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "a";
                    break;
                case 2:
                    manAccImg.sprite = manAccs[1];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "b";
                    break;
                case 3:
                    manAccImg.sprite = manAccs[2];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "c";
                    break;
                case 4:
                    manAccImg.sprite = manAccs[3];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "d";
                    break;
                case 5:
                    manAccImg.sprite = manAccs[4];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "e";
                    break;
                case 6:
                    manAccImg.sprite = manAccs[5];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "f";
                    break;
                case 7:
                    manAccImg.sprite = manAccs[6];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "g";
                    break;
                case 8:
                    manAccImg.sprite = manAccs[7];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "h";
                    break;
                case 9:
                    manAccImg.sprite = manAccs[8];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "i";
                    break;
                case 10:
                    manAccImg.sprite = manAccs[9];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "j";
                    break;
                case 11:
                    manAccImg.sprite = manAccs[10];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "k";
                    break;
                case 12:
                    manAccImg.sprite = manAccs[11];
                    manAccText.text = $"악세사리{manAccsNum}";
                    acceAlpha = "l";
                    break;
            }

            yield return new WaitForSeconds(0);
        }
    }

    public string _saveName;
    public InputField nameInput;
    // 결정 버튼 클릭
    public void _PushCheck()
    {
        if (nameInput.text.Length < 3)
        {
            return;
        }
        PlayerPrefs.SetString("AvatarNumber", $"{faceAlpha}{hairAlpha}{eyeAlpha}{acceAlpha}");
        _saveName = $"{faceAlpha}{hairAlpha}{eyeAlpha}{acceAlpha}{nameInput.text}";

        StopCoroutine(faceCoroutine);
        StopCoroutine(hairCoroutine);
        StopCoroutine(eyeCoroutine);
        StopCoroutine(accCoroutine);

        manFaceImg = null;
        manFaceText = null;
        manHairImg = null;
        manBackHairImg = null;
        manHairText = null;
        manEyeImg = null;
        manEyeText = null;
        manAccImg = null;
        manAccText = null;

        StartCoroutine(ChangeScene());
    }

    GameObject[] _faceObjects;
    GameObject[] _hairObjects;
    GameObject[] _backhairObjects;
    GameObject[] _eyeObjects;
    GameObject[] _accObjects;
    GameObject[] _hair;
    GameObject[] _backhair;

    IEnumerator ChangeScene()
    {
        SceneManager.LoadScene(2);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 2);

        FindObjects();
    }
    void FindObjects()
    {
        //ranking = GameObject.Find("Ranking").GetComponent<Ranking>();
        ranking = GameObject.FindGameObjectWithTag("RANKING").GetComponent<Ranking>();


        _faceObjects = GameObject.FindGameObjectsWithTag("FACEIMAGE");
        _hairObjects = GameObject.FindGameObjectsWithTag("HAIRIMAGE");
        _backhairObjects = GameObject.FindGameObjectsWithTag("BACKHAIRIMAGE");
        _eyeObjects = GameObject.FindGameObjectsWithTag("EYEIMAGE");
        _accObjects = GameObject.FindGameObjectsWithTag("ACCIMAGE");

        for (int i = 0; i < _faceObjects.Length; i++)
        {
            avatarFace[i] = _faceObjects[i].GetComponent<Image>();
        }
        for (int i = 0; i < _hairObjects.Length; i++)
        {
            avatarHair[i] = _hairObjects[i].GetComponent<Image>();
        }
        for (int i = 0; i < _backhairObjects.Length; i++)
        {
            avatarBlackHair[i] = _backhairObjects[i].GetComponent<Image>();
        }
        for (int i = 0; i < _eyeObjects.Length; i++)
        {
            avatarEye[i] = _eyeObjects[i].GetComponent<Image>();
        }
        for (int i = 0; i < _accObjects.Length; i++)
        {
            avatarAcce[i] = _accObjects[i].GetComponent<Image>();
        }

        _hair = GameObject.FindGameObjectsWithTag("HAIRIMAGE");
        _backhair = GameObject.FindGameObjectsWithTag("BACKHAIRIMAGE");

        for (int i = 0; i < _hair.Length; i++)
        {
            hairObjects[i] = _hair[i];
        }
        for (int i = 0; i < _backhair.Length; i++)
        {
            blackHairObjects[i] = _backhair[i];
        }

        OffBackHairObjects();
    }
    void OffBackHairObjects()
    {
        for (int i = 0; i < _backhairObjects.Length; i++)
        {
            _backhairObjects[i].SetActive(false);
        }
    }


    // 각 부분 별 버튼 클릭
    public void _PrevFaceNum()
    {
        manFacesNum -= 1;
        if (manFacesNum == 0)
        {
            manFacesNum = 6;
        }
    }
    public void _NextFaceNum()
    {
        manFacesNum += 1;
        if (manFacesNum == 7)
        {
            manFacesNum = 1;
        }
    }


    public void _PrevHairNum()
    {
        manHairNum -= 1;
        if (manHairNum == 0)
        {
            manHairNum = 15;
        }

    }
    public void _NextHairNum()
    {
        manHairNum += 1;
        if (manHairNum == 16)
        {
            manHairNum = 1;
        }
    }


    public void _PrevEyeNum()
    {
        manEyesNum -= 1;
        if (manEyesNum == 0)
        {
            manEyesNum = 6;
        }

    }
    public void _NextEyeNum()
    {
        manEyesNum += 1;
        if (manEyesNum == 7)
        {
            manEyesNum = 1;
        }
    }


    public void _PrevAccNum()
    {
        manAccsNum -= 1;
        if (manAccsNum == 0)
        {
            manAccsNum = 12;
        }

    }
    public void _NextAccNum()
    {
        manAccsNum += 1;
        if (manAccsNum == 13)
        {
            manAccsNum = 1;
        }
    }


    ////// 불러오기 

    public string rankAvatarFaceAlpha;
    public string rankAvatarHairAlpha;
    public string rankAvatarEyeAlpha;
    public string rankAvatarAcceAlpha;


    public GameObject[] hairObjects;
    public GameObject[] blackHairObjects;

    public Image[] avatarFace;
    public Image[] avatarHair;
    public Image[] avatarBlackHair;
    public Image[] avatarEye;
    public Image[] avatarAcce;

    public Sprite[] faceSprite;
    public Sprite[] hairSprite;
    public Sprite[] blackHairSprite;
    public Sprite[] eyeSprite;
    public Sprite[] acceSprite;

    public Ranking ranking;

    // 게임이 끝났을 때, 이 함수가 실행되게 해야함.
    // for 문 10 -> 7
    // 주석처리가 원본.
    public void ShowRankBoard()
    {
        for (int i = 0; i < 7; i++)
        {
            ranking.bestName[i] = PlayerPrefs.GetString(i.ToString() + "BestName");
            ranking.rankNameText[i].text = string.Format(ranking.bestName[i].Substring(4, 3));

            rankAvatarFaceAlpha = ranking.bestName[i].Substring(0, 1);
            if (rankAvatarFaceAlpha == "a")
            {
                avatarFace[i].sprite = faceSprite[0];
            }
            if (rankAvatarFaceAlpha == "b")
            {
                avatarFace[i].sprite = faceSprite[1];
            }
            if (rankAvatarFaceAlpha == "c")
            {
                avatarFace[i].sprite = faceSprite[2];
            }
            if (rankAvatarFaceAlpha == "d")
            {
                avatarFace[i].sprite = faceSprite[3];
            }
            if (rankAvatarFaceAlpha == "e")
            {
                avatarFace[i].sprite = faceSprite[4];
            }
            if (rankAvatarFaceAlpha == "f")
            {
                avatarFace[i].sprite = faceSprite[5];
            }

            rankAvatarHairAlpha = ranking.bestName[i].Substring(1, 1);
            if (rankAvatarHairAlpha == "a")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[0];
            }
            if (rankAvatarHairAlpha == "b")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[1];
            }
            if (rankAvatarHairAlpha == "c")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[2];
            }
            if (rankAvatarHairAlpha == "d")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[3];
            }
            if (rankAvatarHairAlpha == "e")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[4];
            }
            if (rankAvatarHairAlpha == "f")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[5];
            }
            if (rankAvatarHairAlpha == "g")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[6];
            }
            if (rankAvatarHairAlpha == "h")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[7];
            }
            if (rankAvatarHairAlpha == "i")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[8];
            }
            if (rankAvatarHairAlpha == "j")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[9];
            }
            if (rankAvatarHairAlpha == "k")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[10];
            }
            if (rankAvatarHairAlpha == "l")
            {
                blackHairObjects[i].SetActive(false);
                hairObjects[i].SetActive(true);
                avatarHair[i].sprite = hairSprite[11];
            }
            if (rankAvatarHairAlpha == "m")
            {
                hairObjects[i].SetActive(false);
                blackHairObjects[i].SetActive(true);
                avatarBlackHair[i].sprite = blackHairSprite[0];
            }
            if (rankAvatarHairAlpha == "n")
            {
                hairObjects[i].SetActive(false);
                blackHairObjects[i].SetActive(true);
                avatarBlackHair[i].sprite = blackHairSprite[1];
            }
            if (rankAvatarHairAlpha == "o")
            {
                hairObjects[i].SetActive(false);
                blackHairObjects[i].SetActive(true);
                avatarBlackHair[i].sprite = blackHairSprite[2];
            }

            rankAvatarEyeAlpha = ranking.bestName[i].Substring(2, 1);
            if (rankAvatarEyeAlpha == "a")
            {
                avatarEye[i].sprite = eyeSprite[0];
            }
            if (rankAvatarEyeAlpha == "b")
            {
                avatarEye[i].sprite = eyeSprite[1];
            }
            if (rankAvatarEyeAlpha == "c")
            {
                avatarEye[i].sprite = eyeSprite[2];
            }
            if (rankAvatarEyeAlpha == "d")
            {
                avatarEye[i].sprite = eyeSprite[3];
            }
            if (rankAvatarEyeAlpha == "e")
            {
                avatarEye[i].sprite = eyeSprite[4];
            }
            if (rankAvatarEyeAlpha == "f")
            {
                avatarEye[i].sprite = eyeSprite[5];
            }

            rankAvatarAcceAlpha = ranking.bestName[i].Substring(3, 1);
            if (rankAvatarAcceAlpha == "a")
            {
                avatarAcce[i].sprite = acceSprite[0];
            }
            if (rankAvatarAcceAlpha == "b")
            {
                avatarAcce[i].sprite = acceSprite[1];
            }
            if (rankAvatarAcceAlpha == "c")
            {
                avatarAcce[i].sprite = acceSprite[2];
            }
            if (rankAvatarAcceAlpha == "d")
            {
                avatarAcce[i].sprite = acceSprite[3];
            }
            if (rankAvatarAcceAlpha == "e")
            {
                avatarAcce[i].sprite = acceSprite[4];
            }
            if (rankAvatarAcceAlpha == "f")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "g")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "h")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "i")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "j")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "k")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "l")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "m")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "n")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }
            if (rankAvatarAcceAlpha == "o")
            {
                avatarAcce[i].sprite = acceSprite[5];
            }

            ranking.rankScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            ranking.rankScoreText[i].text = ranking.rankScore[i].ToString();
        }


    }
}
