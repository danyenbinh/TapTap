using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaggleTemplate.Comm;
using UnityEngine;

public class TTControl
{
	//singleton
	private static TTControl m_api;

    public static TTControl Api
    {
        get
        {
            if (m_api == null)
            {
                m_api = new TTControl();
            }
            return m_api;
        }
    }

    private Action<bool, TTLoadingType> m_showLoadingEvent; //notify show hide mood rate popup
    public Action<bool, TTLoadingType> ShowLoadingEvent
    {
        get { return m_showLoadingEvent; }
        set { m_showLoadingEvent = value; }
    }

    private Action<TTPopupIconType, string, bool, Action> m_showPopupEvent; //notify show popup message
    public Action<TTPopupIconType, string, bool, Action> ShowPopupEvent
    {
        get { return m_showPopupEvent; }
        set { m_showPopupEvent = value; }
    }

	public void Init()
	{
		//init other controls here
		TTDownloadControl.Api = new TTDownloadControl();
        TTHomeControl.Api = new TTHomeControl();
        TTGamePlayControl.Api = new TTGamePlayControl();

		ShowLoading(true);
        TTModel.Api.ParseLoadConfig();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        else
        {
           AppBridge.Instance.CallOnPlaygroundLoaded(OnPlaygroundLoaded);
           
        }
	}

	//show hide loading
    //isShow: true = show, false = hide
    public void ShowLoading(bool isShow, TTLoadingType type = TTLoadingType.Default)
    {
        ShowLoadingEvent?.Invoke(isShow,type);//call action
    }

	 //show popup message
    public void ShowPopup(TTPopupIconType iconType, string content, bool isAutoTurnOff, Action callback)
    {
        ShowPopupEvent?.Invoke(iconType,content,isAutoTurnOff,callback);
    }
		
	//playground of the user for current app will auto be loaded.
	private void OnPlaygroundLoaded(Playground pg, APIUnity api)
    {
        // cache it locally
        TTAPIService.Api = api;

        //TODO: using for load simulate configs from file. Replace it after integrate with api
        SimulateLoadConfigData(()=>{
			OnFinishLoadConfigData();
		});

        //load configs by api
        // CoroutineHelper.Call(TTAPIService.Api.GetAppOwnInfo((result)=>{
        //     JObject data = result.Data.MobileConfig;
		// 	TTModel.api.Init(data);
            //OnFinishLoadConfigData();
        // }));
    }

	private void OnFinishLoadConfigData()
    {
        //show scene loader to download resource
        TTDownloadControl.Api.ShowLoader();
    }

	private void SimulateLoadConfigData(Action callback)
	{
		//simulate load data from mockup
        // string json = ResourceObject.GetResource<TextAsset>(TTConstant.PATH_MOCKUP_CONFIG_DATA).text;
		// JObject data = JsonConvert.DeserializeObject<JObject>(json);

		// TTModel.Api.Init(data);

		callback?.Invoke();
	}
}