using System;
using System.Collections.Generic;
using System.Linq;
using TravelCards.Entities;

namespace TravelCards.Managers.TravelSolver
{
    /// <summary>
    /// Travel Fragment Entity. Contains ordered sequence of TravelCards (moves from 1 place to another)
    /// </summary>
    public class TravelFragment : ICloneable
    {
        public string StartPoint
        {
            get { return _data.First().From; }
        }
        public string EndPoint
        {
            get { return _data.Last().To; }
        }

        // travel sequence
        private readonly List<TravelCard> _data = new List<TravelCard>();

        /// <summary>
        /// private .ctor for cloning
        /// </summary>
        private TravelFragment(List<TravelCard> sourceData)
        {
            _data = sourceData;
        }

        /// <summary>
        /// .ctor
        /// </summary>
        public TravelFragment(TravelCard firstCard)
        {
            Add(firstCard);
        }

        public List<TravelCard> GetDataCopy()
        {
            return new List<TravelCard>(_data);
        }

        public void Add(TravelCard item)
        {
            _data.Add(item);
        }

        public void AddRange(IEnumerable<TravelCard> addData)
        {
            _data.AddRange(addData);
        }

        public void AddFragmentData(TravelFragment addFragment)
        {
            _data.AddRange(addFragment.GetDataCopy());
        }

        public object Clone()
        {
            return new TravelFragment(_data.Select(item => item.Clone() as TravelCard).ToList());
        }

        #region Equals & HashCode
        public override bool Equals(object obj)
        {
            return Equals(obj as TravelFragment);
        }

        /// <summary>
        /// compare element by element with another instance Data collection
        /// equal content = equal collections = equal objects
        /// </summary>
        public bool Equals(TravelFragment other)
        {
            if (other == null)
                return false;

            
            var otherData = other.GetDataCopy();
            if (_data.Count != otherData.Count)
            {
                return false;
            }

            bool equals = true;
            for ( int idx = 0; idx < _data.Count; idx++ )
            {
                equals &= _data[idx].Equals(otherData[idx]);
            }
            return equals;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var dataItem in _data)
                {
                    hash = hash * 31 + dataItem.GetHashCode();
                }
                return hash;
            }
        }
        #endregion
    }
}
