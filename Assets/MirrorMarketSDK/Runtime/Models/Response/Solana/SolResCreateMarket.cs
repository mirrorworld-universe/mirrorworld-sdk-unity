using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResCreateMarket
    {
        public SolMarketplaceData marketplace;
        public string transactionStatus;
        public string signature;
    }

    [System.Serializable]
    public class SolMarketplaceData
    {
        public string name;
        public List<string> collections;
        public string auction_house;
        public string authority;
        public string auction_house_treasury;
        public string auction_house_fee_account;
        public string fee_withdrawal_destination;
        public string treasury_mint;
        public string treasury_withdrawal_destination;
        public int seller_fee_basis_points;
        public string signature;
    }

}