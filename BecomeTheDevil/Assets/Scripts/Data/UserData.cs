using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserData
{
    // 데이터들의 세이브&로드를 위한 추상 클래스

    private static BayatGames.SaveGameFree.Serializers.SaveGameJsonSerializer serializer = new BayatGames.SaveGameFree.Serializers.SaveGameJsonSerializer();
        
    public void Save<T>(string identifier, T data)
    {
        BayatGames.SaveGameFree.SaveGame.Save<T>(identifier, data, serializer);
    }

    public T Load<T>(string identifier)
    {
        if (BayatGames.SaveGameFree.SaveGame.Exists(identifier))
            return BayatGames.SaveGameFree.SaveGame.Load<T>(identifier, serializer);
        else return default(T);
    }

    public void Delete(string identifier)
    {
        if (BayatGames.SaveGameFree.SaveGame.Exists(identifier)) BayatGames.SaveGameFree.SaveGame.Delete(identifier);
    }

    public abstract void SaveData();
    public abstract void LoadData();
    public abstract void DeleteData();
}
