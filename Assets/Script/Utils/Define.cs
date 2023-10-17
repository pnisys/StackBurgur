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
        None,
        Tutorial,
        Level1,
        Level2,
        Level3,
        Level4,
    }

    public enum Cards
    {
        None,
        Burgur,
        Source
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
}
