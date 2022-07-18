using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TTGamePlayConfigDTO 
{
    [JsonProperty(PropertyName = TTServiceKey.POINT_PER_HIT)]
    public int PointPerHit { get; set; }
    [JsonProperty(PropertyName = TTServiceKey.TIME)]
    public int Time { get; set; }
    [JsonProperty(PropertyName = TTServiceKey.TIME_3)]
    public int Time3 { get; set; }
}
