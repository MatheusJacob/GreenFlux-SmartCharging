using GreenFlux.SmartCharging.Matheus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ChargeStation(Guid id)
        {
            _availableSlots = new HashSet<int>(Enumerable.Range(ChargeStation.MinConnectors, ChargeStation.MaxConnectors));
            Connectors = new HashSet<Connector>(new ConnectorComparer());
            Id = id;
        }

        public ChargeStation(Guid id, string name, ICollection<Connector> connectors)
        {
            Id = id;
            Name = name;
            _availableSlots = new HashSet<int>(Enumerable.Range(ChargeStation.MinConnectors, ChargeStation.MaxConnectors));
            Connectors = new HashSet<Connector>(new ConnectorComparer());
            this.AppendConnectors(connectors);
        }

        public ChargeStation(Guid id, string name, Connector connector)
        {
            Id = id;
            Name = name;
            _availableSlots = new HashSet<int>(Enumerable.Range(ChargeStation.MinConnectors, ChargeStation.MaxConnectors));
            Connectors = new HashSet<Connector>(new ConnectorComparer());
            this.AppendConnector(connector);
        }

        public ChargeStation(string name, ICollection<Connector> connectors)
        {
            Name = name;
            _availableSlots = new HashSet<int>(Enumerable.Range(ChargeStation.MinConnectors, ChargeStation.MaxConnectors));
            Connectors = new HashSet<Connector>(new ConnectorComparer());
            this.AppendConnectors(connectors);
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
            if (_availableSlots.Count == 0)
                throw new NoSlotsAvailableException(Id);

            if (!connector.Id.HasValue)
            {
                connector.Id = _availableSlots.First();
            }

            _availableSlots.Remove(connector.Id.Value);
            this.UpdateTotalMaxCurrentAmp(connector.MaxCurrentAmp);
            this.Connectors.Add(connector);
        }

        //For some reason constructor isn't getting the Id
        public void SyncConnectorIds()
        {
            foreach (Connector connector in this.Connectors)
            {
                if (connector.Id.HasValue)
                    _availableSlots.Remove(connector.Id.Value);
            }
        }

        public void UpdateTotalMaxCurrentAmp(float maxCurrentAmp)
        {
            this.TotalMaxCurrentAmp += maxCurrentAmp;
        }

        public override bool Equals(object obj)
        {
            var chargeStation = obj as ChargeStation;

            if (chargeStation == null)
            {
                return false;
            }

            return this.Id.Equals(chargeStation.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }

public class ChargeStationComparer : IEqualityComparer<ChargeStation>
    {
        public bool Equals(ChargeStation a, ChargeStation b)
        {
            return a.Id.Equals(b.Id);
        }

        public int GetHashCode(ChargeStation obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
