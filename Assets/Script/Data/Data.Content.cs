using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AvatarStat
{
    public AvatarStat(string id) { Id = id; }

    public int skinNumber = 0;
    public int eyesNumber = 0;
    public int hairsNumber = 0;
    public int accsNumber = 0;

    public string Id;
}

[Serializable]
public class AvatarStatData
{
    public List<AvatarStat> stats = new List<AvatarStat>();
}