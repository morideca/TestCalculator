using UnityEngine;
using History;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] 
	private CalculatorView calculatorView;
	private CalculatorModel calculatorModel;
	private CalculatorPresenter calculatorPresenter;
	
	[SerializeField]
	private HistoryView historyView;
	private HistoryModel historyModel;
	private HistoryPresenter historyPresenter;

	private void Awake()
	{
		calculatorModel = new();
		calculatorPresenter = new(calculatorModel, calculatorView);
		historyModel = new(calculatorModel);
		historyPresenter = new(historyModel, historyView);
		
		calculatorModel.LoadData();
		historyModel.LoadData();
	}
}
