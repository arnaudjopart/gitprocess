using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class JSONExplorer : MonoBehaviour
{
    //private static List<KeyValuePair<string,JToken>> m_metaDataEntriesList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GitAPI.GetStatus());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   
}
