using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoader
{
	private string jsonPath;
	
	public SaveLoader()
	{
		Init();
	}

	private void Init()
	{
		jsonPath = Application.dataPath + "/../Data.json";
	}
	
	public void Save(CalculatorState operationHistory)
	{
		string jsonData = JsonUtility.ToJson(operationHistory);
		File.WriteAllText(jsonPath, jsonData);
	}

	public CalculatorState Load()
	{
		if (File.Exists(jsonPath))
		{
			var jsonData = File.ReadAllText(jsonPath);
			var data = JsonUtility.FromJson<CalculatorState>(jsonData);
			return data;
		}
		else return null;
	}
}