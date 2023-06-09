using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    //[System.Serializable]
    //public class SUIResGetMintedCollections
    //{
    //    public List<SUIResGetMintedCollectionsObj> 
    //}

    [System.Serializable]
    public class SUIResGetMintedCollectionsObj
    {
        public string authority_address;
        public string client_id;
        public string collection_cap_id;
        public string collection_cap_owner;
        public string contract_config_address;
        public string contract_object_id;
        public List<string> creators;
        public string description;
        public string digest;
        public string fee_payer;
        public string mint_cap_id;
        public string mint_cap_owner;
        public string name;
        public long user_id;
    }
}
