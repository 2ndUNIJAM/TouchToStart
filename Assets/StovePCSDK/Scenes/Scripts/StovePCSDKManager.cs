using Stove.PCSDK.NET;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using TouchToStart.Utility;
using UnityEngine;
using UnityEngine.UI;

public class StovePCSDKManager : Singleton<StovePCSDKManager>
{
    // Setting value filled through 'LoadConfig'
    public string Env;
    public string AppKey;
    public string AppSecret;
    public string GameId;
    public StovePCLogLevel LogLevel;
    public string LogPath;
    
    private StovePCCallback callback;
    private Coroutine runcallbackCoroutine;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        ButtonInitialize_Click();
    }

    private void OnDestroy()
    {
        if (runcallbackCoroutine != null)
        {
            StopCoroutine(runcallbackCoroutine);
            runcallbackCoroutine = null;
        }

        StovePC.Uninitialize();
    }

    #region Event Handlers
    public void ButtonLoadConfig_Click()
    {
        string configFilePath = Application.streamingAssetsPath + "/Text/StovePCConfig.Unity.txt";

        if (File.Exists(configFilePath))
        {
            string configText = File.ReadAllText(configFilePath);
            StovePCConfig config = JsonUtility.FromJson<StovePCConfig>(configText);

            this.Env = config.Env;
            this.AppKey = config.AppKey;
            this.AppSecret = config.AppSecret;
            this.GameId = config.GameId;
            this.LogLevel = config.LogLevel;
            this.LogPath = config.LogPath;

            WriteLog(configText);
        }
        else
        {
            string msg = String.Format("File not found : {0}", configFilePath);
            WriteLog(msg);
        }
    }

    public void ToggleRunCallback_ValueChanged(bool isOn)
    {
        if (isOn)
        {
            float intervalSeconds = 1f;
            runcallbackCoroutine = StartCoroutine(RunCallback(intervalSeconds));

            WriteLog("RunCallback Start");
        }
        else
        {
            if (runcallbackCoroutine != null)
            {
                StopCoroutine(runcallbackCoroutine);
                runcallbackCoroutine = null;

                WriteLog("RunCallback Stop");
            }
        }
    }
    #endregion


    #region Coroutine
    private IEnumerator RunCallback(float intervalSeconds)
    {
        WaitForSeconds wfs = new WaitForSeconds(intervalSeconds);
        while (true)
        {
            StovePC.RunCallback();
            yield return wfs;
        }
    }
    #endregion


    #region Methods
    private void WriteLog(string functionName, StovePCResult result)
    {
        if (String.IsNullOrEmpty(functionName))
            functionName = "Unknown";

        string msg = String.Format("{0} Success", functionName);
        if (result != StovePCResult.NoError)
        {
            msg = String.Format("{0} Fail : {1}", functionName, result.ToString());
        }

        Debug.Log(msg + Environment.NewLine);

        AppendUILog(msg);
    }
    private void WriteLog(string log)
    {
        Debug.Log(log + Environment.NewLine);

        AppendUILog(log);
    }

    private void AppendUILog(string log)
    {
        GameObject content = GameObject.Find("ContentLog");
        GameObject textLog = content.transform.GetChild(0).gameObject;
        GameObject copyLog = Instantiate<GameObject>(textLog, content.transform);
        var copyTextComponent = copyLog.GetComponent<Text>();
        copyTextComponent.text = log;
    }

    private void ToggleRunCallback(bool isOn)
    {
        GameObject toggleRunCallback = GameObject.Find("ToggleRunCallback");
        Toggle toggleComponent = toggleRunCallback.GetComponent<Toggle>();
        toggleComponent.isOn = isOn;
    }
    #endregion


    #region SDK Callback Methods
    private void OnError(StovePCError error)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("OnError");
        sb.AppendFormat(" - error.FunctionType : {0}" + Environment.NewLine, error.FunctionType.ToString());
        sb.AppendFormat(" - error.Result : {0}" + Environment.NewLine, error.Result.ToString());
        sb.AppendFormat(" - error.Message : {0}" + Environment.NewLine, error.Message);
        sb.AppendFormat(" - error.ExternalError : {0}", error.ExternalError.ToString());

        WriteLog(sb.ToString());
    }

    private void BeginQuitAppDueToError() {
        #region Log
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("BeginQuitAppDueToError");
        sb.AppendFormat(" - nothing");
        WriteLog(sb.ToString());
        #endregion

        Application.Quit();
    }

    #endregion


    #region SDK Initialization
    public void ButtonInitialize_Click()
    {
        WriteLog("Here I Come");
        StovePCResult sdkResult = StovePCResult.NoError;

        StovePCConfig config = new StovePCConfig
        {
            Env = this.Env,
            AppKey = this.AppKey,
            AppSecret = this.AppSecret,
            GameId = this.GameId,
            LogLevel = this.LogLevel,
            LogPath = this.LogPath
        };

        this.callback = new StovePCCallback
        {
            OnError = new StovePCErrorDelegate(this.OnError),
            OnInitializationComplete = new StovePCInitializationCompleteDelegate(this.OnInitializationComplete),
            OnToken = new StovePCTokenDelegate(this.OnToken),
            OnUser = new StovePCUserDelegate(this.OnUser)
        };

        sdkResult = StovePC.Initialize(config, callback);

        WriteLog("Initialize", sdkResult);

        if (sdkResult == StovePCResult.NoError)
        {
            runcallbackCoroutine = StartCoroutine(RunCallback(0.5f));
        } else {
            BeginQuitAppDueToError();
        }
    }

    private void OnInitializationComplete()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("OnInitializationComplete");
        sb.AppendFormat(" - nothing");

        WriteLog(sb.ToString());
    }
    #endregion


    #region SDK Termination
    public void ButtonUninitialize_Click()
    {
        ToggleRunCallback(false);

        StovePCResult sdkResult = StovePCResult.NoError;

        sdkResult = StovePC.Uninitialize();

        WriteLog("Uninitialize", sdkResult);
    }
    #endregion


    #region Acquiring User Information
    public void ButtonGetUser_Click()
    {
        StovePCResult sdkResult = StovePCResult.NoError;

        sdkResult = StovePC.GetUser();

        WriteLog("GetUser", sdkResult);
    }

    private void OnUser(StovePCUser user)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("OnUser");
        sb.AppendFormat(" - user.MemberNo : {0}" + Environment.NewLine, user.MemberNo.ToString());
        sb.AppendFormat(" - user.Nickname : {0}" + Environment.NewLine, user.Nickname);
        sb.AppendFormat(" - user.GameUserId : {0}", user.GameUserId);

        WriteLog(sb.ToString());
    }
    #endregion


    #region Acquiring Token Information
    public void ButtonGetToken_Click()
    {
        StovePCResult sdkResult = StovePCResult.NoError;

        sdkResult = StovePC.GetToken();

        WriteLog("GetToken", sdkResult);
    }

    private void OnToken(StovePCToken token)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("OnToken");
        sb.AppendFormat(" - token.AccessToken : {0}", token.AccessToken);

        WriteLog(sb.ToString());
    }
    #endregion

    public void RecordMaxStage(int maxStage)
    {
        StovePCResult result = StovePC.SetStat("MAX_LEVEL_CLEARED", maxStage);
        AchievementUI.instance.AchieveVarUpdate();
    }

    public void RecordPressStart(int numStart)
    {
        StovePCResult result = StovePC.SetStat("NUM_PRESS_START", numStart);
        AchievementUI.instance.AchieveVarUpdate();
    }

    public void RecordPressDel(int numDel)
    {
        StovePCResult result = StovePC.SetStat("NUM_PRESS_DEL", numDel);
        AchievementUI.instance.AchieveVarUpdate();
    }

    public void RecordLastStart(int numLStart)
    {
        StovePCResult result = StovePC.SetStat("NUM_PRESS_LAST", numLStart);
        AchievementUI.instance.AchieveVarUpdate();
    }

}
