using System.Collections.Generic;
using UnityEngine;

public class Presenter
{
	private Model model;
	private View view;
	
	public Presenter(Model model, View view)
	{
		this.model = model;
		this.view = view;
		Init();
	}

	private void Init()
	{
		view.onInputSubmitted += OnInputSubmitted;
		view.OnQuitted += OnQuitted;
		model.OnCalculated += OnCalculated;
		model.OnWrongInput += OnWrongInput;
		model.OnLoaded += OnLoaded;
	}

	private void OnInputSubmitted(string input)
	{
		model.ReadInput(input);
	}

	private void OnCalculated()
	{
		view.OnCalculated(model.Data.OperationHistory);
	}

	private void OnWrongInput()
	{
		view.ShowInfo(model.Data.OperationHistory, model.Data.LastInput);
		view.ShowHideErrorScreen();
	}

	private void OnLoaded()
	{
		view.ShowInfo(model.Data.OperationHistory, model.Data.LastInput);
	}

	private void OnQuitted(string lastInput)
	{
		model.OnQuitted(lastInput);
	}
}
