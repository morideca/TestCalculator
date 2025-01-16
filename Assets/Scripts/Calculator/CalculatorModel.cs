using System;

public class CalculatorModel
{
	public event Action OnCalculated;
	public event Action OnWrongInput;
	public event Action<string, string> OnSavedOperationToHistory;
	public event Action OnLoaded;
	public event Action OnQuitted;

	public CalculatorState CalculatorState { get; private set; }
	private SaveLoader<CalculatorState> saveLoader = new("CalculatorState.json");
	
	public void LoadData()
	{
		CalculatorState = saveLoader.Load();
		if (CalculatorState == null)
		{
			CalculatorState = new CalculatorState();
		}
		else
		{
			OnLoaded?.Invoke();
		}
	}
	
	public void OnQuit(string lastInput)
	{
		SaveLastInput(lastInput);
		SaveData();
		OnQuitted?.Invoke();
	}

	public void ReadInput(string input)
	{
		var result = Calculate(input).ToString();
		OnSavedOperationToHistory?.Invoke(input, result);
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
			OnSavedOperationToHistory?.Invoke(input, "ERROR");
			SaveLastInput(input);
			OnWrongInput?.Invoke();
			throw;
		}
	}

	private void SaveLastInput(string input)
	{
		CalculatorState.LastInput = input;
	}
	
	private void SaveData()
	{
		saveLoader.Save(CalculatorState);
	}
}
