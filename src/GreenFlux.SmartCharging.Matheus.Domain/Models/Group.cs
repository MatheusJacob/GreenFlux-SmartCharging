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
            //Todo exception handle + return the options to delete
            if ((CalculateGroupSumCurrentAmp() + chargeStation.TotalMaxCurrentAmp) > this.Capacity)
                throw new Exception("Capacity overload");

            this.ChargeStations.Add(chargeStation);
        }

        public float CalculateGroupSumCurrentAmp()
        {
            float totalCurrentAmp = 0f;
            foreach (ChargeStation chargeStation in ChargeStations)
            {
                foreach (Connector connector in chargeStation.Connectors)
                {
                    totalCurrentAmp += connector.MaxCurrentAmp;
                }
            }
            return totalCurrentAmp;
        }
    }
}
