using System;
using System.Collections;
using System.Collections.Generic;
using Stove.PCSDK.NET;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class StovePCCallBack
{
    // callback : error on StovePCSDK
    public StovePCErrorDelegate OnError;
    // callback : initialize complete
    public StovePCInitializationCompleteDelegate OnInitializationComplete;
    // callback : complete on get token
    public StovePCTokenDelegate OnToken;
    // callback : complete on get user
    public StovePCUserDelegate OnUser;
    // callback : complete on get ownership
    public StovePCOwnershipDelegate OnOnwership;
}

public class StovePCSDKManager : MonoBehaviour
{

    private Coroutine runcallbackCoroutine;

    [SerializeField]
    private static string env;
    private static string appKey;
    private static string appSecret;
    private static string gameId;
    private static StovePCLogLevel logLevel;
    static string logPath;

    StovePCConfig config = new StovePCConfig
    {
        Env = env,
        AppKey = appKey,
        AppSecret = appSecret,
        GameId = gameId,
        LogLevel = logLevel,
        LogPath = logPath,
    };


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    

    private IEnumerator RunCallBack(float intervalSeconds)
    {
        WaitForSeconds wfs = new WaitForSeconds(intervalSeconds);
        while (true)
        {
            StovePC.RunCallback();
            yield return wfs;
        }
    }

    public void ToggleRunCallback_ValueChanged(bool isOn)
    {
        if (isOn) 
        {
            float intervalSeconds = 1.0f;
            runcallbackCoroutine = StartCoroutine(RunCallBack(intervalSeconds));
        }
        else
        {
            if (runcallbackCoroutine != null)
            {
                StopCoroutine(runcallbackCoroutine);
                runcallbackCoroutine = null;
            }
        }
    }


}
