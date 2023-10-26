using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Customer,
        BurgurCard,
        SourceCard
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum SceneType
    {
        Unknown,
        Lobby,
        Tutorial,
        Game
    }

    public enum Mode
    {
        Test,
        Game
    }
    public enum UIEvent
    {
        Click,
        Highlight,
    }

    public enum CustomerState
    {
        Spawned, // 손님이 매장 안에서 처음 소환된 상태, 매대 앞까지 걸어가기 전 대기 상태
        WalkingToCounter, // 손님이 매대 앞까지 걸어오는 상태
        WaitingAtCounter, // 손님이 매대 앞에서 멈추고 주문을 하는 상태
        Judgeing, // 주문이 끝나고 점수가 좋으면 자리에 앉고, 나쁘면 문밖으로 감
        Finish
    }

    public enum ConvertDict
    {
        Key,
        Value
    }

    #region Path
    public const string materialSpriteFolderPath = "Art/Image/BurgurMaterialsSprite/";
    public const string burgurSpriteFolderPath_Tutorial = "Art/Image/BurgurImageSprite/";
    public const string burgurSpriteFolderPath_Level1 = "Art/Image/BurgurImageSprite/1stage/";
    public const string burgurSpriteFolderPath_Level2 = "Art/Image/BurgurImageSprite/2stage/";
    public const string burgurSpriteFolderPath_Level3 = "Art/Image/BurgurImageSprite/3stage/";
    public const string burgurSpriteFolderPath_Level4 = "Art/Image/BurgurImageSprite/4stage/";

    public const string sourceSpriteFolderPath = "Art/Image/SourceCardMaterialsSprite/";
    #endregion

}
