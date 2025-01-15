using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHandler
{
	private string jsonPath;
	
	public DataHandler()
	{
		Init();
	}

	private void Init()
	{
		jsonPath = Application.dataPath + "/../Data.json";
	}
	
	public void Save(Data operationHistory)
	{
		string jsonData = JsonUtility.ToJson(operationHistory);
		File.WriteAllText(jsonPath, jsonData);
	}

	public Data Load()
	{
		if (File.Exists(jsonPath))
		{
			var jsonData = File.ReadAllText(jsonPath);
			var data = JsonUtility.FromJson<Data>(jsonData);
			return data;
		}
		else return null;
	}
}