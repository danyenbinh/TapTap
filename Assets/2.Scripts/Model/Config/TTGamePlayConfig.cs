using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTGamePlayConfig : MonoBehaviour
{
    private TTGamePlayConfigDTO m_GamePlayConfigDTO;

    public void ParseData(string data)
    {
        m_GamePlayConfigDTO = JsonConvert.DeserializeObject<TTGamePlayConfigDTO>(data);      
    }

    public TTGamePlayConfigDTO GetGamePlayConfigDTO()
    {
        //var dictGamePlayItemDTO = new Dictionary<TAGamePlayItemTypeMode, TAGamePlayItemDTO>();
        //foreach (var item in m_dictGamePlayItemDTO)
        //{
        //    dictGamePlayItemDTO.Add(item.Key, item.Value);
        //}
        return m_GamePlayConfigDTO;
    }
}
