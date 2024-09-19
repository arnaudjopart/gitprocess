using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JSONExplorer : MonoBehaviour
{
    private static List<KeyValuePair<string,JToken>> m_metaDataEntriesList;

    // Start is called before the first frame update
    void Start()
    {
        var data = Resources.Load("jsonTest-MetaData") as TextAsset;
        var myJson = data.text;

        ReadMetadataJson(data);
            
        print(m_metaDataEntriesList.Count);
        foreach (var VARIABLE in m_metaDataEntriesList)
        {
            print(VARIABLE.Key);
            var propertiesData = "{"+VARIABLE.Value+"}";
            var objectPropertiesDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(propertiesData);
            if (objectPropertiesDictionary.TryGetValue("Properties", out var properties))
            {
                print(properties.Count);
                foreach (var entry in properties)
                {
                    print(entry.Key+" - "+entry.Value);
                }
            }

        }
        
        
        /*JObject rss = JObject.Parse(myJson);
        
        var propertyLength = rss["ObjectsMetadata"][1]["Properties"].Children().Count();
        var properties = "{"+rss["ObjectsMetadata"][1].Last+"}";
        var objectId = "{"+rss["ObjectsMetadata"][1].First+"}";
        var idDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(objectId);
        if (idDictionary.TryGetValue("ObjectId", out var id))
        {
            print(id);
        }
        print(properties);
        var response = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(properties);
        foreach (var VARIABLE in response.Values)
        {
          foreach (var subVARIABLE in VARIABLE)
          {
            print(subVARIABLE.Key);
          }
        }
        print(propertyLength);*/
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void ReadMetadataJson(TextAsset _json)
    {
        var jsonObject = JObject.Parse(_json.text);
        m_metaDataEntriesList = new List<KeyValuePair<string, JToken>>();
        
        foreach (var node in jsonObject["ObjectsMetadata"].Children())
        {
            var objectId = "{"+node.First+"}";
            var idDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(objectId);
            if (idDictionary.TryGetValue("ObjectId", out var id))
            {
                m_metaDataEntriesList.Add(new KeyValuePair<string, JToken>(id,node.Last));
            }
        }
    }
}
