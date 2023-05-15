using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResMetadataGetCollectionSummary
    {
        public string collection;
        public string collection_name;
        public string collection_owner;
        public int nft_amount;
        public int listed_amount;
        public string floor_price;
    }
}