using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DishControl : MonoBehaviour
{
    public List<GameObject> finaldishs = new List<GameObject>();
    public GameObject[] food;
    int index = 0;
    private void OnEnable()
    {
        TutorialMeatControl.OnTraying += this.DishcontrolMethod;
        TutorialTrayControl.OnTraying += this.DishcontrolMethod;
        FoodControl.OnDishing += this.DishcontrolMethod;
    }
    private void OnDisable()
    {
        TutorialMeatControl.OnTraying -= this.DishcontrolMethod;
        TutorialTrayControl.OnTraying -= this.DishcontrolMethod;
        FoodControl.OnDishing -= this.DishcontrolMethod;
    }

    void DishcontrolMethod()
    {
        StartCoroutine(Realdishcontrolmethod());
    }

    //이건 트레이에서 소환되는 프리팹
    IEnumerator Realdishcontrolmethod()
    {
        index = 0;
        yield return new WaitForSeconds(0.3f);
        //1.dishcheck은 12개의 접시 중에서 자식이 없는 첫번째 요소를 반환한다.
        var dishcheck = finaldishs.Where(n => n.transform.childCount == 0).FirstOrDefault();
        //2. index는 자식이 없는 첫번째 요소의 인덱스다.
        try
        {
            index = finaldishs.FindIndex(n => n.transform.childCount == 0);
        }
        //이건 index값을 못찾으면 그냥 나온은 에러인듯
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("접시 다 차 있음");
        }
        try
        {
            //3. 프리팹을 소환한다.
            GameObject newcreatematerial = Instantiate(food[index], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), dishcheck.transform);
            //4. 소환 후 포지션값과 위치값을 맞춰준다.
            newcreatematerial.transform.localPosition = new Vector3(0, 0, 0);
            newcreatematerial.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("접시 다 차 있음");
        }
    }
}
