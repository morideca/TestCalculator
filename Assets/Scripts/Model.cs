using System;
using System.Collections.Generic;

public class Model
{
	public event Action OnCalculated;
	public event Action OnWrongInput;
	public event Action OnLoaded;

	public Data Data { get; private set; }
	private DataHandler dataHandler;

	public Model(DataHandler dataHandler)
	{
		this.dataHandler = dataHandler;
	}
	
	public void LoadData()
	{
		Data = dataHandler.Load();
		if (Data == null)
		{
			Data = new Data();
		}
		else
		{
			OnLoaded?.Invoke();
		}
	}
	
	public void OnQuitted(string lastInput)
	{
		SaveLastInput(lastInput);
		SaveData();
	}

	public void ReadInput(string input)
	{
		var result = Calculate(input).ToString();
		SaveOperationToHistory(input, result);
		OnCalculated?.Invoke();
	}
	
	private int Calculate(string input)
	{
		try
		{
			int operatorIndex = input.IndexOf('+');

			if (operatorIndex > 0 && operatorIndex < input.Length - 1)
			{
				string firstNumber = input.Substring(0, operatorIndex).Trim();
				string secondNumber = input.Substring(operatorIndex + 1).Trim();

				if (int.TryParse(firstNumber, out int num1) && int.TryParse(secondNumber, out int num2))
				{
					return num1 + num2;
				}
			}
			
			throw new ArgumentException("invalid input");
		}
		catch (Exception e)
		{
			SaveOperationToHistory(input, "ERROR");
			SaveLastInput(input);
			OnWrongInput?.Invoke();
			throw;
		}
	}

	private void SaveLastInput(string input)
	{
		Data.LastInput = input;
	}

	private void SaveOperationToHistory(string input, string result)
	{
		var stringResult = input + $"={result}";
		Data.OperationHistory.Add(stringResult);
	}
	
	private void SaveData()
	{
		dataHandler.Save(Data);
	}
}
