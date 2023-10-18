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
    public enum Scene
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
        바베큐소스,
        칠리소스,
        머스타드소스,
        마요네즈소스
    }

    public enum CustomerState
    {
        Spawned, // 손님이 매장 안에서 처음 소환된 상태, 매대 앞까지 걸어가기 전 대기 상태
        WalkingToCounter, // 손님이 매대 앞까지 걸어오는 상태
        WaitingAtCounter, // 손님이 매대 앞에서 멈추고 주문을 기다리는 상태
        Ordering // 손님이 주문을 하는 상태
    }

    public enum TutorialBurgurNames
    {
        데리버거,
    }

    public enum Level1BurgurNames
    {
        새우버거,
        토토버거,
        토마토패티버거,
        더블치즈버거,
        베이컨버거,
        머쉬룸버거,
        촉촉한버거,
        불고기버거
    }
    public enum Level2BurgurNames
    {
        치즈불고기버거,
        샌드위치햄버거,
        치킨버거,
        트리플고기버거,
        먹물버거,
        양파버거,
        육즙가득버거,
        버섯많이버거
    }
    public enum Level3BurgurNames
    {
        살찌는버거,
        치즈토마토버거,
        샌드위치치즈버거,
        다이어트버거,
        고칼로리버거,
        더블새우버거,
        직화버섯불고기버거,
        치즈새우버거
    }
    public enum Level4BurgurNames
    {
        기네스버거,
        수분가득버거,
        통큰버거,
        비주얼버거,
        폭탄칼로리버거,
        일편단심버거,
        기력보충버거,
        빵많이버거
    }



}
