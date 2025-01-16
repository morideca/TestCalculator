using System.IO;
using UnityEngine;

public class SaveLoader<T>
{
    private readonly string jsonPath;
	
    public SaveLoader(string jsonPath)
    {
        this.jsonPath = Path.Combine(Application.dataPath, jsonPath);
    }

    public void Save(T type)
    {
        string jsonData = JsonUtility.ToJson(type);
        File.WriteAllText(jsonPath, jsonData);
    }

    public T Load()
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