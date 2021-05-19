using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;


[Serializable]
public class WebImageData 
{
    [JsonProperty(propertyName: "id")] public string Id { get; set; }
    [JsonProperty(propertyName: "author")] public string Author { get; set; }
    [JsonProperty(propertyName: "width")] public int Width { get; set; }
    [JsonProperty(propertyName: "height")] public int Height { get; set; }
    [JsonProperty(propertyName: "url")] public string Url { get; set; }
    [JsonProperty(propertyName: "download_url")] public string DownloadUrl { get; set; }
}
