using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, AvatarStat> StatDict = new Dictionary<string, AvatarStat>();

    public void AddAvatarStat(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        if (!StatDict.ContainsKey(id))
        {
            AvatarStat avatarStat = new AvatarStat();
            StatDict.Add(id, avatarStat);
        }
    }

    public AvatarStat GetAvatarStat(string id)
    {
        if (string.IsNullOrEmpty(id))
            return null;

        if (StatDict.ContainsKey(id))
        {
            return StatDict[id];
        }

        return null;
    }
}
