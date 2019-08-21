﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework.Config;

public class UILoginWindow : UIWindow 
{
    [SerializeField]
    Button btnLogIn;
    [SerializeField]
    Button btnConfig;
    [SerializeField]
    Button btnLocalization;
    [SerializeField]
    Button btnEvent;

    void Awake()
    {
        this.btnLogIn.onClick.AddListener(this.OnLoginButtonClicked);
        this.btnConfig.onClick.AddListener(this.OnConfigButtonClicked);
        this.btnLocalization.onClick.AddListener(this.OnLocalizationButtonClicked);
        this.btnEvent.onClick.AddListener(this.OnEventButtonClicked);
    }

    private void OnLoginButtonClicked()
    {
        UIManager.Instance.PushWindow("UIHome");
    }

    private void OnConfigButtonClicked()
    {
        ConfigManager.Instance.LoadConfigs(OnLoadConfigsProgressCallback, OnLoadConfigsSuccessCallback, OnLoadConfigsFailureCallback);
        btnConfig.interactable = false;
    }

    private void OnLocalizationButtonClicked()
    {
        UIManager.Instance.PushWindow("UILocalization");
    }

    private void OnEventButtonClicked()
    {
        UIManager.Instance.PushWindow("UIEventCenter");
    }

    private void OnLoadConfigsProgressCallback(string configTableName)
    {
        Debug.Log("===========config:" + configTableName);
    }

    private void OnLoadConfigsFailureCallback(string configTableName, string errMessage)
    {
        Debug.LogError("================err:" + errMessage);
    }

    private void OnLoadConfigsSuccessCallback()
    {
        Debug.Log("==============load config success");
        
        var cfgRow = ConfigManager.Instance.GetConfigRow<ConfigPropRow>(101);
        if (cfgRow != null)
        {
            Debug.LogFormat("id:{0}, limit:{1}, tex: {2}", cfgRow.Id, cfgRow.Limit, cfgRow.Tex);
        }
    }
}
