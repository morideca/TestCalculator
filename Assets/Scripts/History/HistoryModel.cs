using System;

namespace History
{
    public class HistoryModel
    {
        public event Action OnOperationSaved;
        public event Action OnLoaded;
        
        public HistoryState HistoryState { get; private set; } = new();
        
        private readonly CalculatorModel calculatorModel;
        private SaveLoader<HistoryState> saveLoader = new("HistoryState.json");

        public HistoryModel(CalculatorModel calculatorModel)
        {
            this.calculatorModel = calculatorModel;
            Init();
        }
        
        public void LoadData()
        {
            HistoryState = saveLoader.Load();
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
            saveLoader.Save(HistoryState);
        }
    }
}