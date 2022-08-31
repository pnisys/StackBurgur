using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FisrtChoice : MonoBehaviour
{
    public void TutorialStart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(2);
    }

}
