using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResGetTransactions
    {
        public List<SolResGetTransactionsTransaction> transactions;
        public int count;
        public string next_before;
    }

    [System.Serializable]
    public class SolResGetTransactionsTransaction
    {
        public long blockTime;
        public SolResGetTransactionsTransactionMeta meta;
        public long slot;
        public SolResGetTransactionsTransactionMessage transaction;
    }

    [System.Serializable]
    public class SolResGetTransactionsTransactionMeta
    {
        public int computeUnitsConsumed;
        public object err;
        public int fee;
        public List<SolResGetTransactionsInnerInstruction> innerInstructions;
        public List<string> logMessages;
        public List<long> postBalances;
        public List<SolResGetTransactionsPostTokenBalance> postTokenBalances;
        public List<long> preBalances;
        public List<SolResGetTransactionsPreTokenBalance> preTokenBalances;
        public List<object> rewards;
        public SolResGetTransactionsStatus status;
    }

    [System.Serializable]
    public class SolResGetTransactionsInnerInstruction
    {
        public int index;
        public List<SolResGetTransactionsInstruction> instructions;
        public string program;
        public string programId;
    }

    [System.Serializable]
    public class SolResGetTransactionsInstruction
    {
        public SolResGetTransactionsParsed parsed;
        public string type;
        public string program;
        public string programId;
    }

    [System.Serializable]
    public class SolResGetTransactionsParsed
    {
        public SolResGetTransactionsInfo info;
        public string type;
    }

    [System.Serializable]
    public class SolResGetTransactionsInfo
    {
        public string owner;
        public string source;
    }

    [System.Serializable]
    public class SolResGetTransactionsPostTokenBalance
    {
        public int accountIndex;
        public string mint;
        public string owner;
        public string programId;
        public SolResGetTransactionsUiTokenAmount uiTokenAmount;
    }

    [System.Serializable]
    public class SolResGetTransactionsPreTokenBalance
    {
        public int accountIndex;
        public string mint;
        public string owner;
        public string programId;
        public SolResGetTransactionsUiTokenAmount uiTokenAmount;
    }

    [System.Serializable]
    public class SolResGetTransactionsUiTokenAmount
    {
        public string amount;
        public int decimals;
        public int uiAmount;
        public string uiAmountString;
    }

    [System.Serializable]
    public class SolResGetTransactionsStatus
    {
        public object Ok;
    }

    [System.Serializable]
    public class SolResGetTransactionsTransactionMessage
    {
        public List<SolResGetTransactionsAccountKey> accountKeys;
        public object addressTableLookups;
        public List<SolResGetTransactionsInstruction> instructions;
        public string recentBlockhash;
        public List<string> signatures;
    }
    [System.Serializable]
    public class SolResGetTransactionsAccountKey
    {
        public string pubkey;
        public bool signer;
        public string source;
        public bool writable;
    }

    [System.Serializable]
    public class TransactionResponse
    {
        public List<SolResGetTransactionsTransaction> transactions;
        public int count;
        public string next_before;
    }

    [System.Serializable]
    public class TransactionResult
    {
        public string status;
        public TransactionResponse data;
        public int code;
        public string message;
    }

}