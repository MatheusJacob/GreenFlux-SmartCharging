using GreenFlux.SmartCharging.Matheus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class Group
    {
        public readonly Guid Id;

        public string Name { get; set; }

        public float Capacity { get; set; }

        private float GroupSumMaxCurrent { get; set; }

        public readonly HashSet<ChargeStation> ChargeStations;

        public Group()
        {
            ChargeStations = new HashSet<ChargeStation>(new ChargeStationComparer());
        }
        public Group(Guid id, string name, float capacity)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            ChargeStations = new HashSet<ChargeStation>(new ChargeStationComparer());
        }

        public void AppendChargeStation(ChargeStation chargeStation)
        {            
            this.ChargeStations.Add(chargeStation);
        }

        public float CalculateGroupSumCurrentAmp()
        {
            float totalCurrentAmp = 0f;
            ////naive implementation
            foreach (ChargeStation chargeStation in ChargeStations)
            {
                foreach (Connector connector in chargeStation.Connectors)
                {
                    totalCurrentAmp += connector.MaxCurrentAmp;
                }
            }
            return totalCurrentAmp;
        }

        public bool HasExceededCapacity(float addedMaxCurrentAmp)
        {
            GroupSumMaxCurrent = (this.CalculateGroupSumCurrentAmp() + addedMaxCurrentAmp);
            return (GroupSumMaxCurrent > this.Capacity);
        }

        public float GetExceededCapacity()
        {
            return Math.Abs(Capacity - GroupSumMaxCurrent);
        }

        public RemoveSuggestions GenerateRemoveSuggestions(float exceededCapacity, List<Connector> connectors)
        {


            return new RemoveSuggestions();
        }

        private List<Connector> FindAllConnectorsSuggestions(float exceededCapacity, List<Connector> connectors)
        {
            float prefix = 0;
            SortedSet<float> set = new SortedSet<float>();
            set.Add(prefix);
            float leastDiff = float.MaxValue;

            List<Connector> results = new List<Connector>();
            foreach (Connector connector in connectors)
            {
                prefix += connector.MaxCurrentAmp; // the cumulative sum up to i

                float rest = prefix - exceededCapacity; // how far away we are from t
                float theSum = 0;

                if (set.First() <= rest)
                {
                    theSum = prefix - set.LastOrDefault(x => x <= rest);
                }
                if (set.Last() >= rest && set.Last() > exceededCapacity)
                {
                    theSum = prefix - set.FirstOrDefault(x => x >= rest);
                }

                if (((theSum - exceededCapacity) >= 0 || rest < 0) && Math.Abs(theSum - exceededCapacity) <= leastDiff)
                {
                    leastDiff = Math.Abs(theSum - exceededCapacity);
                    results.Add(connector);
                }

                set.Add(prefix);
            }
            return results;
        }
    }
}
