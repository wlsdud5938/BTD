using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStatusData : UserData
{
    private bool isBuildMode;

    private const string identifier_isBuildMode = "identifier_isBuildMode";

    public UserStatusData()
    {
        isBuildMode = false;
    }

    public bool IsBuildMode() { return isBuildMode; }

    public void BuildModeOnOff(bool onOff)
    {
        isBuildMode = onOff;
        Save<bool>(identifier_isBuildMode, isBuildMode);
    }

    public void BuildModeOnOff()
    {
        isBuildMode = !isBuildMode;
        Save<bool>(identifier_isBuildMode, isBuildMode);
    }

    public override void SaveData()
    {
        Save<bool>(identifier_isBuildMode, isBuildMode);
    }

    public override void LoadData()
    {
        isBuildMode = Load<bool>(identifier_isBuildMode);
    }

    public override void DeleteData()
    {
        Delete(identifier_isBuildMode);
        isBuildMode = false;
    }
}
