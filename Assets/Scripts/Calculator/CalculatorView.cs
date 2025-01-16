using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CalculatorView : MonoBehaviour
{
	public event Action<string> onInputSubmitted;
	public event Action<string> OnQuitted;

	[SerializeField] 
	private GameObject calculatorScreen;
	[SerializeField] 
	private TMP_InputField inputField;
	[SerializeField]
	private Button resultButton;
	[SerializeField]
	private GameObject errorScreen;
	[SerializeField] 
	private Button errorButton;
	[SerializeField] 
	private Button quitButton;

	private void Awake()
	{
		resultButton.onClick.AddListener(OnInputSubmit);
		errorButton.onClick.AddListener(ShowHideErrorScreen);
		quitButton.onClick.AddListener(Quit);
	}

	public void ShowHideErrorScreen()
	{
		calculatorScreen.SetActive(!calculatorScreen.activeSelf);
		errorScreen.SetActive(!errorScreen.activeSelf);
	}
	
	public void OnCalculated()
	{
		inputField.text = "Enter the equation...";
	}
	
	public void ShowLastInput(string lastInput)
	{
		inputField.text = lastInput;
	}
	
	private void OnInputSubmit()
	{
		string input = inputField.text;
		onInputSubmitted?.Invoke(input);
	}
	
	private void OnDestroy()
	{
		resultButton.onClick.RemoveListener(OnInputSubmit);
	}

	private void Quit()
	{
		OnQuitted?.Invoke(inputField.text);
		Application.Quit();
	}
}
