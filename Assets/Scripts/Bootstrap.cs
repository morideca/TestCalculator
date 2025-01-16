using UnityEngine;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] 
	private View view;
	private SaveLoader _saveLoader;
	private Model model;
	private Presenter presenter;

	private void Awake()
	{
		_saveLoader = new();
		model = new(_saveLoader);
		presenter = new(model, view);
		
		model.LoadData();
	}
}
