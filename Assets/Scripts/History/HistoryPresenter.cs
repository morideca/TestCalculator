using System.Collections.Generic;

namespace History
{
    public class HistoryPresenter
    {
        private HistoryModel model;
        private HistoryView view;
        
        public HistoryPresenter(HistoryModel model, HistoryView view)
        {
            this.model = model;
            this.view = view;
            Init();
        }

        private void Init()
        {
            model.OnOperationSaved += ShowHistory;
            model.OnLoaded += ShowHistory;
        }

        private void ShowHistory()
        {
            view.ShowHistory(model.HistoryState.History);
        }
    }
}