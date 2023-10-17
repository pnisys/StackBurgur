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
}
