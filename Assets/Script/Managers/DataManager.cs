using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, AvatarStat> StatDict = new Dictionary<string, AvatarStat>();

    public void AddAvatarStat(string id, AvatarStat stat)
    {
        if (string.IsNullOrEmpty(id) || stat == null)
            return;

        if (!StatDict.ContainsKey(id))
        {
            StatDict.Add(id, stat);
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
