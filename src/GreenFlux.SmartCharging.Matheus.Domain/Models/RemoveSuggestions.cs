using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class RemoveSuggestions : List<SuggestionList>
    {
        public RemoveSuggestions()
        {

        }
        public void GenerateAllSuggestions(List<Connector> connectors, float exceededCapacity)
        {
            RemoveSuggestions set1 = new RemoveSuggestions();
            RemoveSuggestions set2 = new RemoveSuggestions();
            GetAllsubSequenceSum(0, ((connectors.Count - 1) / 2), connectors, set1, new SuggestionList());
            GetAllsubSequenceSum((((connectors.Count - 1) / 2) + 1), (connectors.Count - 1), connectors, set2, new SuggestionList());
            float min = float.MaxValue;
            set2.Sort();

            for (int i = 0; i < set1.Count; i++)
            {                
                float firstSetSum = set1[i].TotalSum;

                SuggestionList remainingPart = new SuggestionList(exceededCapacity - firstSetSum);
                int pos = set2.BinarySearch(remainingPart);
                if ((pos >= 0))
                {
                    if (min > 0)
                        this.Clear();

                    min = 0;
                    this.Add(new SuggestionList(set1[i], set2[pos]));
                }
                else
                {
                    int position = (1 * (pos + 1)) * -1;
                    int low = (position - 1);
                    if ((low >= 0))
                    {
                        if (firstSetSum + (set2[low].TotalSum) > exceededCapacity)
                        {
                            float absoluteValue = Math.Abs((firstSetSum + (set2[low].TotalSum - exceededCapacity)));
                            if (absoluteValue > min)
                                continue;

                            if (absoluteValue < min)
                            {
                                this.Clear();
                                min = absoluteValue;
                            }

                            this.Add(new SuggestionList(set1[i], set2[low]));

                        }
                    }

                    if ((low != (set2.Count() - 1)))
                    {
                        if (firstSetSum + (set2[position].TotalSum) > exceededCapacity)
                        {
                            float absoluteValue = Math.Abs((firstSetSum + (set2[position].TotalSum - exceededCapacity)));
                            if (absoluteValue > min)
                                continue;

                            if (absoluteValue < min)
                            {
                                this.Clear();
                                min = absoluteValue;
                            }

                            this.Add(new SuggestionList(set1[i], set2[position]));

                            ////This is needed because binary search just return the first value found
                            if (set2[position].TotalSum != 0)
                                this.GenerateDuplicatedSuggestions(position, set1[i], set2, set2[position].TotalSum);
                        }
                    }
                }
            }
        }

        private void GenerateDuplicatedSuggestions(int initialPosition, SuggestionList set1, RemoveSuggestions set2, float totalSum)
        {
            for (int i = initialPosition; i < set2.Count; i++)
            {
                if (set2[i].TotalSum != totalSum)
                    break;

                this.Add(new SuggestionList(set1, set2[i]));
            }
        }
        public void GetAllsubSequenceSum(int initial, int length, List<Connector> connectors, RemoveSuggestions removeSuggestions, SuggestionList suggestionList)
        {
            if ((initial == (length + 1)))
            {
                removeSuggestions.Add(suggestionList);
                return;
            }

            this.GetAllsubSequenceSum((initial + 1), length, connectors, removeSuggestions, new SuggestionList(suggestionList, connectors[initial]));
            this.GetAllsubSequenceSum((initial + 1), length, connectors, removeSuggestions, suggestionList);
        }
    }
}
