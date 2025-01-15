using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class View : MonoBehaviour
{
	public event Action<string> onInputSubmitted;
	public event Action<string> OnQuitted;

	[SerializeField] 
	private GameObject calculatorScreen;
	[SerializeField] 
	private TMP_InputField inputField;
	[SerializeField]
	private Transform scrollViewContent;
	[SerializeField]
	private GameObject historyTextLinePrebaf;
	[SerializeField]
	private Button resultButton;
	[SerializeField]
	private GameObject errorScreen;
	[SerializeField] 
	private Button errorButton;
	[SerializeField] 
	private Button quitButton;

	private List<GameObject> historyGOList = new();

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
	
	public void OnCalculated(List<string> history)
	{
		ShowHistory(history);
		inputField.text = "Enter the equation...";
	}

	public void ShowInfo(List<string> history, string lastInput)
	{
		ShowLastInput(lastInput);
		ShowHistory(history);
	}
	
	private void ShowLastInput(string lastInput)
	{
		inputField.text = lastInput;
	}

	private void ShowHistory(List<string> history)
	{
		ClearHistoryGO();
		var historyView = new List<string>(history);
		historyView.Reverse();
		foreach (var line in historyView)
		{
			var textInstance = Instantiate(historyTextLinePrebaf, scrollViewContent);
			historyGOList.Add(textInstance);
			var text = textInstance.GetComponent<TMP_Text>();
			text.text = line;
		}
	}
	
	private void ClearHistoryGO()
	{
		foreach (var line in historyGOList)
		{
			Destroy(line);
		}
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
