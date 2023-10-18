using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditateManager
{
    public void Notify()
    {
        BurgurCardController burgurCard = Managers.Tutorial.Spawn(Define.WorldObject.BurgurCard, "Card_Burgur").GetComponent<BurgurCardController>();
        burgurCard.transform.position = new Vector3(0.9900001f, -0.572f, 0.013f);
        burgurCard.transform.Rotate(new Vector3(0f, 180f, 0f));
        burgurCard.BurgurName = Define.TutorialBurgurNames.데리버거.ToString();
        burgurCard.InitBurgurCard();

        SourceCardController sourceCard = Managers.Tutorial.Spawn(Define.WorldObject.BurgurCard, "Card_Source").GetComponent<SourceCardController>();
        sourceCard.transform.position = new Vector3(0.397f, -0.572f, 0.013f);
        sourceCard.transform.Rotate(new Vector3(0f, 180f, 0f));
        Array sourceNames = Enum.GetValues(typeof(Define.SourceNames));
        int randomValue = UnityEngine.Random.Range(0, sourceNames.Length);
        sourceCard.CurrentSource = (Define.SourceNames)sourceNames.GetValue(randomValue);
    }
}
