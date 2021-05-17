using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class ChargeStation
    {
        public const int MaxConnectors = 5;
        public const int MinConnectors = 1;
        private readonly HashSet<int> _availableSlots;

        public readonly Guid Id;

        public String Name { get; set; }

        public HashSet<Connector> Connectors { get; set; }

        public float TotalMaxCurrentAmp { get; private set; } = 0f;
        public Group Group { get; set; }

        public Guid GroupId { get; set; }

        public ChargeStation()
        {
            _availableSlots = new HashSet<int>(Enumerable.Range(ChargeStation.MinConnectors, ChargeStation.MaxConnectors));
            Connectors = new HashSet<Connector>(new ConnectorComparer());
        }

        public ChargeStation(string name, ICollection<Connector> connectors)
        {
            Name = name;
            _availableSlots = new HashSet<int>(Enumerable.Range(ChargeStation.MinConnectors, ChargeStation.MaxConnectors));
            Connectors = new HashSet<Connector>(new ConnectorComparer());
            this.AppendConnectors(connectors);
        }

        public float GetSumMaxCurrentAmp()
        {
            float sumMaxCurrentAmp = 0;
            foreach (var connector in this.Connectors)
            {
                sumMaxCurrentAmp += connector.MaxCurrentAmp;
            }

            return sumMaxCurrentAmp;
        }
        public void AppendConnectors(ICollection<Connector> connectors)
        {
            foreach (var connector in connectors)
            {
                this.AppendConnector(connector);
            }            
        }

        public void AppendConnector(Connector connector)
        {
            //TODO create specific exception
            if (_availableSlots.Count == 0)
                throw new Exception("No available slots");

            if (!connector.Id.HasValue)
            {
                connector.Id = _availableSlots.First();
            }

            _availableSlots.Remove(connector.Id.Value);
            //connector.ChargeStationId = this.Id;
            this.TotalMaxCurrentAmp += connector.MaxCurrentAmp;
            this.Connectors.Add(connector);
        }
    }
}
