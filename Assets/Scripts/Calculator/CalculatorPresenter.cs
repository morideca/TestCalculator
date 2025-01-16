public class CalculatorPresenter
{
	private CalculatorModel calculatorModel;
	private CalculatorView calculatorView;
	
	public CalculatorPresenter(CalculatorModel calculatorModel, CalculatorView calculatorView)
	{
		this.calculatorModel = calculatorModel;
		this.calculatorView = calculatorView;
		Init();
	}

	private void Init()
	{
		calculatorView.onInputSubmitted += OnInputSubmitted;
		calculatorView.OnQuitted += OnQuitted;
		calculatorModel.OnCalculated += OnCalculated;
		calculatorModel.OnWrongInput += OnWrongInput;
		calculatorModel.OnLoaded += OnLoaded;
	}

	private void OnInputSubmitted(string input)
	{
		calculatorModel.ReadInput(input);
	}

	private void OnCalculated()
	{
		calculatorView.OnCalculated();
	}

	private void OnWrongInput()
	{
		calculatorView.ShowLastInput(calculatorModel.CalculatorState.LastInput);
		calculatorView.ShowHideErrorScreen();
	}

	private void OnLoaded()
	{
		calculatorView.ShowLastInput(calculatorModel.CalculatorState.LastInput);
	}

	private void OnQuitted(string lastInput)
	{
		calculatorModel.OnQuit(lastInput);
	}
}
