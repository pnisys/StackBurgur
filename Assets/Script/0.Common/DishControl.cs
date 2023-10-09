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

    //�̰� Ʈ���̿��� ��ȯ�Ǵ� ������
    IEnumerator Realdishcontrolmethod()
    {
        index = 0;
        yield return new WaitForSeconds(0.3f);
        //1.dishcheck�� 12���� ���� �߿��� �ڽ��� ���� ù��° ��Ҹ� ��ȯ�Ѵ�.
        var dishcheck = finaldishs.Where(n => n.transform.childCount == 0).FirstOrDefault();
        //2. index�� �ڽ��� ���� ù��° ����� �ε�����.
        try
        {
            index = finaldishs.FindIndex(n => n.transform.childCount == 0);
        }
        //�̰� index���� ��ã���� �׳� ������ �����ε�
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("���� �� �� ����");
        }
        try
        {
            //3. �������� ��ȯ�Ѵ�.
            GameObject newcreatematerial = Instantiate(food[index], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), dishcheck.transform);
            //4. ��ȯ �� �����ǰ��� ��ġ���� �����ش�.
            newcreatematerial.transform.localPosition = new Vector3(0, 0, 0);
            newcreatematerial.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("���� �� �� ����");
        }
    }
}
