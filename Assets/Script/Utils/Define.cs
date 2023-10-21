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

    public enum Levels
    {
        Tutorial,
        Level1,
        Level2,
        Level3,
        Level4,
    }

    public enum SourceNames
    {
        �ٺ�ť�ҽ�,
        ĥ���ҽ�,
        �ӽ�Ÿ��ҽ�,
        �������ҽ�
    }

    public enum CustomerState
    {
        Spawned, // �մ��� ���� �ȿ��� ó�� ��ȯ�� ����, �Ŵ� �ձ��� �ɾ�� �� ��� ����
        WalkingToCounter, // �մ��� �Ŵ� �ձ��� �ɾ���� ����
        WaitingAtCounter, // �մ��� �Ŵ� �տ��� ���߰� �ֹ��� ��ٸ��� ����
        Ordering // �մ��� �ֹ��� �ϴ� ����
    }

    public enum TutorialBurgurNames
    {
        ��������,
    }

    public enum Level1BurgurNames
    {
        �������,
        �������,
        �丶����Ƽ����,
        ����ġ�����,
        ����������,
        �ӽ������,
        �����ѹ���,
        �Ұ�����
    }
    public enum Level2BurgurNames
    {
        ġ��Ұ�����,
        ������ġ�ܹ���,
        ġŲ����,
        Ʈ���ð�����,
        �Թ�����,
        ���Ĺ���,
        ���󰡵����,
        �������̹���
    }
    public enum Level3BurgurNames
    {
        ����¹���,
        ġ���丶�����,
        ������ġġ�����,
        ���̾�Ʈ����,
        ��Į�θ�����,
        ����������,
        ��ȭ�����Ұ�����,
        ġ��������
    }
    public enum Level4BurgurNames
    {
        ��׽�����,
        ���а������,
        ��ū����,
        ���־����,
        ��źĮ�θ�����,
        ����ܽɹ���,
        ��º������,
        �����̹���
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
