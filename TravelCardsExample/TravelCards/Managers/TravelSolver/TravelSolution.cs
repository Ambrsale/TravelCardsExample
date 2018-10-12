using System;
using System.Collections.Generic;
using System.Linq;
using TravelCards.Entities;

namespace TravelCards.Managers.TravelSolver
{
    /// <summary>
    /// Processes over given parameters and tries to find first correct travel.
    /// If successfully solved - Result field contains solution, AllResults contains all possible non-doubled solutions.
    /// 
    /// Solving method - group all given cards over TRAVEL FROM.
    /// All the groupings, having > 1 member are temporary roots, then try to build unambiguous way from them stopping on optional joins.
    /// 
    /// After all groupings > 1 are processed, we have alot of travel fragments (some of them maybe 1 point length)
    /// Then we try to group fragments unambiguous way (trivial unoptional ways).
    /// 
    /// After all unambigous groupings are done we have alotof travel fragments, some of them have different variants of joining.
    /// 
    /// Here we try to apply all the variants, collecting matched results and throwing away non-matched
    /// (some of the joins may be incorrect and result to unjoinable travels)
    /// This resolving is done over recursive calls to TravelSolution.
    /// </summary>
    public class TravelSolution
    {
        /// <summary>
        /// First computed correct travel version or null if impossible to solve given parameters.
        /// </summary>
        public TravelFragment Result { get { return AllResults.FirstOrDefault(); } }

        /// <summary>
        /// All possible solutions.
        /// </summary>
        public List<TravelFragment> AllResults = new List<TravelFragment>();

        // sorted correct travel fragments
        private List<TravelFragment> _travelFragments = new List<TravelFragment>();

        // unprocessed travelpoints, key = FROM value of these cards.
        private Dictionary<string, List<TravelCard>> _travelPointsDic;

        public TravelSolution(List<TravelCard> travelCards)
        {
            // convert list to dictionary over FROM point 
            _travelPointsDic = travelCards.GroupBy(item => item.From).ToDictionary(item => item.Key, item => item.ToList());
        }

        private TravelSolution(List<TravelFragment> travelFragments)
        {
            _travelFragments = travelFragments;
            _travelPointsDic = new Dictionary<string, List<TravelCard>>();
        }

        /// <summary>
        /// Try to solve travel over given parameters
        /// </summary>
        public void Solve()
        {
            // create fragments over all possible junctions
            _travelPointsDic.Where(item => item.Value.Count > 1)
                .ToList().ForEach(item => createFragmentsOverJunction(item.Key));

            // add leftover points as fragments
            _travelFragments.AddRange(_travelPointsDic.Values.Select(item => new TravelFragment(item.First())));

            // combine fragments
            combineFragments();

            if (_travelFragments.Count == 1)
            {
                AllResults.Add(_travelFragments.First());
            }
            else
            {
                AllResults.AddRange(tryMultivariantCombining());
            }
        }

        /// <summary>
        /// Creates no-branches way over allowed points from given start
        /// </summary>
        private TravelFragment tryBuildFragmentFrom(TravelCard startPoint)
        {
            if (startPoint == null)
            {
                return null;
            }

            TravelFragment result = new TravelFragment(startPoint);
            TravelCard lastFoundPoint = startPoint;

            // while next step is only one possible - keep moving
            while (_travelPointsDic.ContainsKey(lastFoundPoint.To)
                // only one possible move
                && _travelPointsDic[lastFoundPoint.To].Count == 1 
                // no fragments as next possible move (could became unresolvable)
                && _travelFragments.All(item => item.StartPoint != lastFoundPoint.To))
            {
                // single point - add and continue
                TravelCard tempPoint = _travelPointsDic[lastFoundPoint.To].First();
                // remove item from possible points
                _travelPointsDic.Remove(lastFoundPoint.To);
                // add to fragment
                result.Add(tempPoint);

                lastFoundPoint = tempPoint;
            }

            return result;
        }

        private void createFragmentsOverJunction(string junctionPointName)
        {
            // keep source data
            List < TravelCard > junctionSource = _travelPointsDic[junctionPointName];

            // remove data from available points
            _travelPointsDic.Remove(junctionPointName);

            // build fragments over source and add them to fragments collection
            _travelFragments.AddRange(junctionSource.Select(item => tryBuildFragmentFrom(item)));
        }

        private void combineFragments()
        {
            // combine all simple variants
            bool isDataCombined = false;
            int processingIdx = 0;
            do
            {
                isDataCombined = false;
                processingIdx = 0;

                // cycle over fragments
                while (processingIdx < _travelFragments.Count())
                {
                    TravelFragment sourceFragment = _travelFragments[processingIdx];
                    // search possible next stop
                    List<TravelFragment> matchedFragments = _travelFragments.Except(new[] { sourceFragment })
                        .Where(item => sourceFragment.EndPoint == item.StartPoint).ToList();

                    // decise which fragment to add
                    TravelFragment fragmentToAdd = null;

                    // single fragment found - take it
                    if (matchedFragments.Count == 1)
                    {
                        fragmentToAdd = matchedFragments.First();
                    }
                    // different options - take any circle fragment if exists
                    else
                    {
                        fragmentToAdd = matchedFragments.FirstOrDefault(item => item.StartPoint == item.EndPoint);
                    }

                    if (fragmentToAdd != null)
                    {
                        sourceFragment.AddFragmentData(matchedFragments.First());
                        _travelFragments.Remove(matchedFragments.First());
                        isDataCombined = true;
                    }

                    processingIdx++;
                }
            }
            while (isDataCombined && _travelFragments.Count() > 1);
        }

        /// <summary>
        /// This method tries each variant of possible ambiguous fragment joins. On the first success it returns the variant found.
        /// </summary>
        private List<TravelFragment> tryMultivariantCombining()
        {
            HashSet<TravelFragment> results = new HashSet<TravelFragment>();

            foreach (TravelFragment sourceFragment in _travelFragments)
            {
                // search possible next stop
                IEnumerable<TravelFragment> matchedFragments = _travelFragments.Except(new[] { sourceFragment })
                    .Where(item => sourceFragment.EndPoint == item.StartPoint);

                foreach (var possibleNextStop in matchedFragments)
                {
                    // create copies of data
                    // join possible branch and try next level of merging

                    List<TravelFragment> fragmentsCopy = _travelFragments.Except(new[] { sourceFragment, possibleNextStop }).Select(item => item.Clone() as TravelFragment).ToList();
                    TravelFragment sourceCopy = sourceFragment.Clone() as TravelFragment;
                    TravelFragment possibleNextStopCopy = possibleNextStop.Clone() as TravelFragment;
                    sourceCopy.AddFragmentData(possibleNextStopCopy);

                    fragmentsCopy.Add(sourceCopy);

                    TravelSolution tsVariant = new TravelSolution(fragmentsCopy);
                    tsVariant.Solve();
                    if (tsVariant.Result != null)
                    {
                        results.UnionWith(tsVariant.AllResults);
                    }
                }
            }
            return results.ToList();
        }
    }
}
