using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class RemoveSuggestions : List<SuggestionList>
    {
        public List<SuggestionList> Suggestions { get; set; }
        public RemoveSuggestions()
        {

        }
        public RemoveSuggestions(List<SuggestionList> suggestions)
        {
            Suggestions = suggestions;

            foreach (List<Suggestion> item in suggestions)
            {
                foreach (var item2 in item)
                {
                    
                }
            }
        }

        public RemoveSuggestions(SuggestionList suggestionList)
        {
            Suggestions = new List<SuggestionList>() { suggestionList };

            foreach (List<Suggestion> item in Suggestions)
            {
                foreach (var item2 in item)
                {

                }
            }
        }
    }
}
