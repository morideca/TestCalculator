using System;

namespace History
{
    public class HistoryModel
    {
        public event Action OnOperationSaved;
        public event Action OnLoaded;
        
        public HistoryState HistoryState { get; private set; } = new();
        
        private readonly CalculatorModel calculatorModel;
        private Save<HistoryState> save = new("HistoryState.json");

        public HistoryModel(CalculatorModel calculatorModel)
        {
            this.calculatorModel = calculatorModel;
            Init();
        }
        
        public void LoadData()
        {
            HistoryState = save.LoadData();
            if (HistoryState == null)
            {
                HistoryState = new();
            }
            else
            {
                OnLoaded?.Invoke();
            }
        }
        
        private void Init()
        {
            calculatorModel.OnSavedOperationToHistory += SaveOperationToHistory;
            calculatorModel.OnQuitted += OnQuit;
        }
        
        private void SaveOperationToHistory(string input, string result)
        {
            var stringResult = input + $"={result}";
            HistoryState.History.Add(stringResult);
            OnOperationSaved?.Invoke();
        }

        private void OnQuit()
        {
            save.SaveData(HistoryState);
        }
    }
}