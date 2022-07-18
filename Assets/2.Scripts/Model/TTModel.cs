using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public enum TTLoadingType
{
	Default,
    WithBackground
}

public enum TTPopupIconType
{
	Normal,
    Success,
    Error
}

public class TTModel
{
	public static TTModel m_api;
	public static TTModel Api
    {
        get
        {
            if(m_api == null)
                m_api = new TTModel();
            return m_api;
        }
    }

    public TTGamePlayConfig GamePlayConfig { get; set; } = new TTGamePlayConfig();

    //configs
    // private Dictionary<long,string> m_appDataConfig; // app config

    // public Dictionary<long,string> AppDataConfig
    // {
    //     get { return m_appDataConfig; }
    //     set { m_appDataConfig = value; }
    // }

    //param
    public bool isLoadProcess;//flag for downloading process

	//init configs
	public void Init(JObject obj)
	{
        // parse app configs
        // ParseAppConfig(obj.SafeValue<JArray>(TTServiceKey.PARAM_APP_CONFIG_DATA));
        // ParseOtherAppConfig(obj.Value<string>(TTServiceKey.PARAM_OTHER_APP_CONFIG_DATA) ?? "");
	}

    public void ParseLoadConfig()
    {
        var config = ResourceObject.GetResource<TextAsset>(TTConstant.TT_GAME_CONFIG);
        GamePlayConfig.ParseData(config.text);
    }


	// parse app config
    // private void ParseAppConfig(JArray data)
    // {
    //     AppDataConfig = new Dictionary<string, TTAppDataConfig>();
    //     foreach (var jToken in data)
    //     {
    //         JObject child = (JObject)jToken;
    //         TTAppDataConfig pr = new TTAppDataConfig(child);
    //         AppDataConfig.Add(pr.id, pr);
    //     }
    // }

	// get appDataDTO by indentify key
    // public TTAppDataDTO GetAppDataDTO(string id)
    // {
    //     if(!AppDataConfig.ContainsKey(id))
    //         return null;

    //     return new TTAppDataDTO(AppDataConfig[id]);
    // }
}