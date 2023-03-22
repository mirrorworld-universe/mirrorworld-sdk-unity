﻿using UnityEngine;
using System.Collections;
using MirrorworldSDK.Wrapper;

namespace MirrorworldSDK
{
    public class UrlUtils
    {

        private static string GetHost(MirrorService service)
        {
            MirrorEnv env = MWClientWrapper.GetEnv();
            if (service == MirrorService.Wallet || belongToAsset(service) || belongToMetadata(service)
        || service == MirrorService.Confirmation)
            {
                if (env == MirrorEnv.Devnet)
                {
                    return "https://api.mirrorworld.fun";
                }
                else if (env == MirrorEnv.Mainnet)
                {
                    return "https://api.mirrorworld.fun";
                }
                else
                {
                    MirrorWrapper.Instance.LogFlow("Unknown env:" + env);
                    return "https://api-staging.mirrorworld.fun";
                }
            }
            else
            {
                if (env == MirrorEnv.Devnet)
                {
                    return "https://auth.mirrorworld.fun";
                }
                else if (env == MirrorEnv.Mainnet)
                {
                    return "https://auth.mirrorworld.fun";
                }
                else
                {
                    MirrorWrapper.Instance.LogFlow("Unknown env:" + env + ".Will use production host.");
                    return "https://auth.mirrorworld.fun";
                }
            }
        }

        private static string GetChainString(MirrorChain chain)
        {
            if (chain == MirrorChain.Solana)
            {
                return "solana";
            }
            else if (chain == MirrorChain.Ethereum)
            {
                return "ethereum";
            }
            else if (chain == MirrorChain.Polygon)
            {
                return "polygon";
            }
            else if (chain == MirrorChain.BNB)
            {
                return "bnb";
            }
            else if (chain == MirrorChain.SUI)
            {
                return "sui";
            }
            else
            {
                MirrorWrapper.Instance.LogFlow("Invalida chain enum:" + chain);
                return "solana";
            }
        }

        private static string GetNetwork(MirrorChain chain,MirrorEnv env)
        {
            if (chain == MirrorChain.Solana)
            {
                if (env == MirrorEnv.Mainnet)
                {
                    return "mainnet-beta";
                }
                else if (env == MirrorEnv.Devnet)
                {
                    return "devnet";
                }
                else
                {
                    MirrorWrapper.Instance.LogFlow("Unknown env:" + env + ".Will use mainnet.");
                    return "devnet";
                }
            }
            else if (chain == MirrorChain.Ethereum)
            {
                if (env == MirrorEnv.Mainnet)
                {
                    return "mainnet";
                }
                else if (env == MirrorEnv.Devnet)
                {
                    return "goerli";
                }
                else
                {
                    MirrorWrapper.Instance.LogFlow("Unknown env:" + env + ".Will use mainnet.");
                    return "goerli";
                }
            }
            else if (chain == MirrorChain.Polygon)
            {
                if (env == MirrorEnv.Mainnet)
                {
                    return "mumbai-mainnet";
                }
                else if (env == MirrorEnv.Devnet)
                {
                    return "mumbai-testnet";
                }
                else
                {
                    MirrorWrapper.Instance.LogFlow("Unknown env:" + env + ".Will use mainnet.");
                    return "mumbai-testnet";
                }
            }
            else if (chain == MirrorChain.BNB)
            {
                if (env == MirrorEnv.Mainnet)
                {
                    return "bnb-mainnet";
                }
                else if (env == MirrorEnv.Devnet)
                {
                    return "bnb-testnet";
                }
                else
                {
                    MirrorWrapper.Instance.LogFlow("Unknown env:" + env + ".Will use mainnet.");
                    return "bnb-testnet";
                }
            }
            else
            {
                MirrorWrapper.Instance.LogFlow("Unknwon chain" + chain);
                return "unknwon-net";
            }
        }

        private static string GetService(MirrorService serviceEnum)
        {
            if (serviceEnum == MirrorService.Marketplace)
            {
                return "marketplaces";
            }
            else if (serviceEnum == MirrorService.Wallet)
            {
                return "wallet";
            }
            else if (serviceEnum == MirrorService.AssetAuction)
            {
                return "asset/auction";
            }
            else if (serviceEnum == MirrorService.AssetMint)
            {
                return "asset/mint";
            }
            else if (serviceEnum == MirrorService.AssetNFT)
            {
                return "asset/nft";
            }
            else if (serviceEnum == MirrorService.Confirmation)
            {
                return "asset/confirmation";
            }
            else if (serviceEnum == MirrorService.Metadata)
            {
                return "metadata";
            }
            else if (serviceEnum == MirrorService.MetadataCollection)
            {
                return "metadata/collection";
            }
            else if (serviceEnum == MirrorService.MetadataNFT)
            {
                return "metadata/nft";
            }
            else if (serviceEnum == MirrorService.MetadataNFTSearch)
            {
                return "metadata/nft/search";
            }
            else
            {
                MirrorWrapper.Instance.LogFlow("Unknown service:" + serviceEnum);
                return "";
            }
        }

        public static string GetMirrorPostUrl(MirrorService serviceEnum, string apiPath)
        {
            MirrorChain chainEnum = MWClientWrapper.GetChain();
            MirrorEnv envEnum = MWClientWrapper.GetEnv();

            string host = GetHost(serviceEnum);
            string version = "v2";
            string chain = GetChainString(chainEnum);
            string network = GetNetwork(chainEnum, envEnum);
            string service = GetService(serviceEnum);

            string finalUrl = host + "/" + version + "/" + chain + "/" + network + "/" + service + "/" + apiPath;
            return finalUrl;
        }

        public static string GetMirrorGetUrl(MirrorService serviceEnum)
        {
            MirrorChain chainEnum = MWClientWrapper.GetChain();
            MirrorEnv envEnum = MWClientWrapper.GetEnv();

            string host = GetHost(serviceEnum);
            string version = "v2";
            string chain = GetChainString(chainEnum);
            string network = GetNetwork(chainEnum, envEnum);
            string service = GetService(serviceEnum);

            string finalUrl = host + "/" + version + "/" + chain + "/" + network + "/" + service + "/";
            return finalUrl;
        }

        private static bool belongToAsset(MirrorService service)
        {
            return service == MirrorService.AssetAuction || service == MirrorService.AssetMint || service == MirrorService.AssetNFT;
        }

        private static bool belongToMetadata(MirrorService service)
        {
            return service == MirrorService.Metadata || service == MirrorService.MetadataNFT
                    || service == MirrorService.MetadataCollection || service == MirrorService.MetadataNFTSearch;
        }
    }
}
