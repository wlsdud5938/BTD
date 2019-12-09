using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStatusData : UserData
{
    [System.Serializable]
    public class HealthInfo
    {
        private float maxHP;
        private float currentHP;

        public HealthInfo(float max, float current)
        {
            maxHP = max;
            currentHP = current;
        }

        public float getMaxHP() { return maxHP; }
        public float getCurrentHP() { return currentHP; }
        public void setMaxHP(float hp) { maxHP = hp; }
        public void setCurrentHP(float hp) { currentHP = hp; }
    }

    private bool isBuildMode;
    private int playingChara;    // 0:그린, 1:화이트, 2:그린 슬라임
    public HealthInfo greenHealth, whiteHealth;

    private const string identifier_isBuildMode = "identifier_isBuildMode";
    private const string identifier_greenHealth = "identifier_greenHealth";
    private const string identifier_whiteHealth = "identifier_whiteHealth";
    private const string identifier_playingChara = "identifier_playingChara";

    public UserStatusData()
    {
        isBuildMode = false;
        playingChara = 0;
        greenHealth = new HealthInfo(100, 100);
        whiteHealth = new HealthInfo(70, 70);

        LoadData();
    }

    public bool IsBuildMode() { return isBuildMode; }
    public int GetPlayingChara() { return playingChara; }

    public HealthInfo GetHealthInfo(int index)
    {
        if (index == 0) return greenHealth;
        else return whiteHealth;
    }

    public HealthInfo GetHealthInfo()
    {
        if (playingChara == 0) return greenHealth;
        else return whiteHealth;
    }

    // 캐릭터가 살아있는지(특정 캐릭터 확인)
    public bool IsCharacterAlive(int index)
    {
        if (index == 0) return greenHealth.getCurrentHP() != 0;
        else if (index == 1) return whiteHealth.getCurrentHP() != 0;

        return false;
    }

    // 캐릭터들이 살아있는지(둘 중 누구라도)
    public bool IsCharacterAlive()
    {
        return greenHealth.getCurrentHP() != 0 || whiteHealth.getCurrentHP() != 0;
    }

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

    public void SetPlayingChara(int index)
    {
        playingChara = index;
        Save<int>(identifier_playingChara, playingChara);
    }

    public void SetHealth(int state, float hp)
    {
        if (state == 0) greenHealth.setCurrentHP(hp);
        if (state == 1) whiteHealth.setCurrentHP(hp);

        SaveData();
    }

    public void SetHealth(float hp)
    {
        if (playingChara == 0) greenHealth.setCurrentHP(hp);
        if (playingChara == 1) whiteHealth.setCurrentHP(hp);

        SaveData();
    }

    public override void SaveData()
    {
        Save<bool>(identifier_isBuildMode, isBuildMode);
        Save<int>(identifier_playingChara, playingChara);
        Save<HealthInfo>(identifier_greenHealth, greenHealth);
        Save<HealthInfo>(identifier_whiteHealth, whiteHealth);
    }

    public override void LoadData()
    {
        isBuildMode = Load<bool>(identifier_isBuildMode);
        playingChara = Load<int>(identifier_playingChara);
        greenHealth = Load<HealthInfo>(identifier_greenHealth);
        whiteHealth = Load<HealthInfo>(identifier_whiteHealth);
    }

    public override void DeleteData()
    {
        Delete(identifier_isBuildMode);
        Delete(identifier_playingChara);
        Delete(identifier_greenHealth);
        Delete(identifier_whiteHealth);

        isBuildMode = false;
        playingChara = 0;
        greenHealth = new HealthInfo(100, 100);
        whiteHealth = new HealthInfo(70, 70);
    }
}
