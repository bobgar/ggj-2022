using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager : MonoBehaviour
{
    public TextAsset stringFile;
    
    private Dictionary<string, string> stringDictionary;
    private const char lineSeperater = '\n'; // It defines line seperate character
    private const char fieldSeperator = ','; // It defines field seperate chracter

    private static StringManager _instance;
    public static StringManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("StringManager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        LoadStrings();
    }

    public void LoadStrings()
    {
        if (stringDictionary != null)
        {
            stringDictionary.Clear();
        }
        else
        {
            stringDictionary = new Dictionary<string, string>();
        }

        string[] records = stringFile.text.Split(lineSeperater);
        for (int i = 1; i < records.Length; i++)
        {
            string[] fields = records[i].Split(fieldSeperator);
            string left = fields[0];
            string right = "";
            for( int j = 1; j < fields.Length; j++)
            {
                right += fields[j];
                if( j < fields.Length-1)
                {
                    right += ",";
                }
            }

            right = right.Replace("\"", "");
            stringDictionary.Add(left, right);
        }
    }

    public string GetString(string key)
    {
        return stringDictionary[key];
    }
}
