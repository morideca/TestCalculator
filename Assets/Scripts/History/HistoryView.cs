using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace History
{
    public class HistoryView : MonoBehaviour
    {
        [SerializeField]
        private Transform scrollViewContent;
        [SerializeField]
        private GameObject historyTextLinePrebaf;

        private List<GameObject> historyGOList = new();
        
        public void ShowHistory(List<string> history)
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
    }
}