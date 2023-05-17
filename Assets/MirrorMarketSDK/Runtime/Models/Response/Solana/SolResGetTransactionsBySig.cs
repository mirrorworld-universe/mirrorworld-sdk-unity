using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResGetTransactionBySig
    {
        public int blockTime;
        public SolResGetTransBySigMeta meta;
        public int slot;
        public SolResGetTransBySigTransaction transaction;
    }

    //transaction
    [System.Serializable]
    public class SolResGetTransBySigAccountKey
    {
        public string pubkey;
        public bool signer;
        public string source;
        public bool writable;
    }

    [System.Serializable]
    public class SolResGetTransBySigInstruction
    {
        public List<string> accounts;
        public string data;
        public string programId;
    }

    [System.Serializable]
    public class SolResGetTransBySigTransactionMessage
    {
        public List<SolResGetTransBySigAccountKey> accountKeys;
        public object addressTableLookups;
        public List<SolResGetTransBySigInstruction> instructions;
        public string recentBlockhash;
    }

    [System.Serializable]
    public class SolResGetTransBySigTransaction
    {
        public SolResGetTransBySigTransactionMessage transaction;
        public List<string> signatures;
    }





    //meta
    [System.Serializable]
    public class SolResGetTransBySigParsed
    {
        public SolResGetTransBySigInfo info;
        public string type;
    }

    [System.Serializable]
    public class SolResGetTransBySigInfo
    {
        public int lamports;
        public string newAccount;
        public string owner;
        public string source;
        public int space;
        public int decimals;
        public string freezeAuthority;
        public string mint;
        public string mintAuthority;
        public string rentSysvar;
        public string account;
        public string systemProgram;
        public string tokenProgram;
        public string wallet;
        public string amount;
        public string destination;
        public string extensionTypes;
        public string authorityType;
        public string multisigAuthority;
        public string newAuthority;
        public List<string> signers;
    }

    [System.Serializable]
    public class SolResGetTransBySigInnerInstruction
    {
        public int index;
        public List<SolResGetTransBySigInstruction> instructions;
    }

    [System.Serializable]
    public class SolResGetTransBySigMeta
    {
        public int computeUnitsConsumed;
        public object err;
        public int fee;
        public List<SolResGetTransBySigInnerInstruction> innerInstructions;
    }

}