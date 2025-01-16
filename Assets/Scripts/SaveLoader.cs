using System.IO;
using UnityEngine;

public class Save<T>
{
    private readonly string jsonPath;
	
    public Save(string jsonPath)
    {
        this.jsonPath = Path.Combine(Application.dataPath, jsonPath);
    }

    public void SaveData(T type)
    {
        string jsonData = JsonUtility.ToJson(type);
        File.WriteAllText(jsonPath, jsonData);
    }

    public T LoadData()
    {
        if (File.Exists(jsonPath))
        {
            var jsonData = File.ReadAllText(jsonPath);
            var data = JsonUtility.FromJson<T>(jsonData);
            return data;
        }

        return default;
    }
}