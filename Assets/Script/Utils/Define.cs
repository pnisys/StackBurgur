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
        Spawned, // �մ��� ���� �ȿ��� ó�� ��ȯ�� ����, �Ŵ� �ձ��� �ɾ�� �� ��� ����
        WalkingToCounter, // �մ��� �Ŵ� �ձ��� �ɾ���� ����
        WaitingAtCounter, // �մ��� �Ŵ� �տ��� ���߰� �ֹ��� �ϴ� ����
        Judgeing, // �ֹ��� ������ ������ ������ �ڸ��� �ɰ�, ���ڸ� �������� ��
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
