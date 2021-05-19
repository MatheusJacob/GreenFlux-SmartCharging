﻿using GreenFlux.SmartCharging.Matheus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            foreach (var connector in connectors)
            {

            }

            Suggestion suggestion = new Suggestion(new Guid(), 1);
            SuggestionList suggestionList = new SuggestionList();
            suggestionList.Add(suggestion);
            suggestionList.Add(suggestion);

            RemoveSuggestions removeSuggestions = new RemoveSuggestions();
            removeSuggestions.Add(suggestionList);
            removeSuggestions.Add(suggestionList);
            removeSuggestions.Add(suggestionList);
            removeSuggestions.Add(suggestionList);


            return removeSuggestions;

        }
    }
}
