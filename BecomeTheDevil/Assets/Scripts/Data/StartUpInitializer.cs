using UnityEngine;
using UnityEngine.Assertions;

public class StartUpInitializer
{
    // 씬 로딩순서와 상관없이 최우선으로 프리팹을 생성할 수 있게 해주는 클래스
    // Resources 폴더 아래 지정된 경로에 있는 프리팹을 게임 실행과 동시에 생성시켜 준다
    // 자세한 내용은 https://cafe.naver.com/unityhub/31617 참고
    
        // 1. 해당 프리팹의 경로 선언
    private const string MANAGER_PATH_UnitInfoManager = "ManagerPrefabs/UnitInfoManager";
    private const string MANAGER_PATH_ItemInfoManager = "ManagerPrefabs/ItemInfoManager";
    private const string MANAGER_PATH_UserDataManager = "ManagerPrefabs/UserDataManager";


    [RuntimeInitializeOnLoadMethodAttribute]
    public static void InitializeManager()
    {
        // 2. 해당 경로로 ManagerInit 함수 호출 
        ManagerInit(MANAGER_PATH_UnitInfoManager);
        ManagerInit(MANAGER_PATH_ItemInfoManager);
        ManagerInit(MANAGER_PATH_UserDataManager);
    }

    public static void ManagerInit(string path)
    {
        GameObject managerPrefab = Resources.Load(path) as GameObject;
        Assert.IsNotNull(managerPrefab, "There is no application manager prefab!!");
        GameObject manager = GameObject.Instantiate(managerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject.DontDestroyOnLoad(manager);
    }
}
