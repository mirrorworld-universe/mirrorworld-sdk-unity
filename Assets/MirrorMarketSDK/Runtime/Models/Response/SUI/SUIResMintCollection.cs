using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIResMintCollection
    {
        public int user_id;
        public string name;
        public string description;
        public List<string> creators;
        public string digest;
        public string contract_config_address;
        public string contract_object_id;
        public string authority_address;
        public string collection_cap_id;
        public string collection_cap_owner;
        public string client_id;
        public string mint_cap_id;
        public string mint_cap_owner;
    }
}