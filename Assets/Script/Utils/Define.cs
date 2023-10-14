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
}
