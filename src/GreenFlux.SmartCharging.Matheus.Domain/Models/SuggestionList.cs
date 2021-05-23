using System;
using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class SuggestionList : List<Suggestion>, IComparable
    {
        public float TotalSum { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            SuggestionList otherSuggestionList = obj as SuggestionList;
            if (otherSuggestionList != null)
                return this.TotalSum.CompareTo(otherSuggestionList.TotalSum);
            else
                throw new ArgumentException("Object is not a SuggestionList");
        }

        public SuggestionList(float totalSum)
        {
            TotalSum = totalSum;
        }

        public SuggestionList()
        {

        }
        public SuggestionList(SuggestionList suggestionList, Connector connectorToAdd)
        {
            this.AddRange(suggestionList);
            this.Add(new Suggestion(connectorToAdd.ChargeStationId, connectorToAdd.Id.Value, connectorToAdd.MaxCurrentAmp));
            TotalSum = suggestionList.TotalSum + connectorToAdd.MaxCurrentAmp;
        }
        public SuggestionList(SuggestionList suggestionList, SuggestionList suggestionList2)
        {
            this.AddRange(suggestionList);
            this.AddRange(suggestionList2);
            TotalSum = suggestionList.TotalSum + suggestionList2.TotalSum;
        }
    }
}
