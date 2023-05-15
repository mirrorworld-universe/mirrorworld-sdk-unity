using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResGetTransactionsByWalletUiTokenAmount
    {
        public string amount;
        public int decimals;
        public int uiAmount;
        public string uiAmountString;
    }

    [System.Serializable]
    public class SolResGetTransactionsByWalletPostTokenBalance
    {
        public int accountIndex;
        public string mint;
        public string owner;
        public string programId;
        public SolResGetTransactionsByWalletUiTokenAmount uiTokenAmount;
    }

    [System.Serializable]
    public class SolResGetTransactionsByWalletMeta
    {
        public int computeUnitsConsumed;
        public object err;
        public int fee;
        public List<object> innerInstructions;
        public List<string> logMessages;
        public List<int> postBalances;
        public List<SolResGetTransactionsByWalletPostTokenBalance> postTokenBalances;
        public List<int> preBalances;
        public List<SolResGetTransactionsByWalletPostTokenBalance> preTokenBalances;
        public List<object> rewards;
        public Dictionary<string, object> status;
    }

    [System.Serializable]
    public class SolResGetTransactionsByWalletAccountKey
    {
        public string pubkey;
        public bool signer;
        public string source;
        public bool writable;
    }

    [System.Serializable]
    public class SolResGetTransactionsByWalletInstruction
    {
        public List<string> accounts;
        public string data;
        public string programId;
    }

    [System.Serializable]
    public class SolResGetTransactionsByWalletMessage
    {
        public List<SolResGetTransactionsByWalletAccountKey> accountKeys;
        public object addressTableLookups;
        public List<SolResGetTransactionsByWalletInstruction> instructions;
        public string recentBlockhash;
    }

    [System.Serializable]
    public class SolResGetTransactionsByWalletTransaction
    {
        public int blockTime;
        public SolResGetTransactionsByWalletMeta meta;
        public int slot;
        public SolResGetTransactionsByWalletMessage message;
    }

    [System.Serializable]
    public class SolResGetTransactionByWallet
    {
        public List<SolResGetTransactionsByWalletTransaction> transactions;
        public int count;
        public string next_before;
    }



}