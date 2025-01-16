using System;
using System.Collections.Generic;

public class Model
{
	public event Action OnCalculated;
	public event Action OnWrongInput;
	public event Action OnLoaded;

	public CalculatorState CalculatorState { get; private set; }
	private SaveLoader _saveLoader;

	public Model(SaveLoader saveLoader)
	{
		this._saveLoader = saveLoader;
	}
	
	public void LoadData()
	{
		CalculatorState = _saveLoader.Load();
		if (CalculatorState == null)
		{
			CalculatorState = new CalculatorState();
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
		CalculatorState.LastInput = input;
	}

	private void SaveOperationToHistory(string input, string result)
	{
		var stringResult = input + $"={result}";
		CalculatorState.OperationHistory.Add(stringResult);
	}
	
	private void SaveData()
	{
		_saveLoader.Save(CalculatorState);
	}
}
