using UnityEngine;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] 
	private View view;
	private DataHandler dataHandler;
	private Model model;
	private Presenter presenter;

	private void Awake()
	{
		dataHandler = new();
		model = new(dataHandler);
		presenter = new(model, view);
		
		model.LoadData();
	}
}
