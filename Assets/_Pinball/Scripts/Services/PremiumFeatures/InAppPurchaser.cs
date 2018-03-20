using UnityEngine;
using System.Collections;

#if EASY_MOBILE
using EasyMobile;
#endif

namespace SgLib
{
    public class InAppPurchaser : MonoBehaviour
    {
        [System.Serializable]
        public struct CoinPack
        {
            public string productName;
            public string priceString;
            public int coinValue;
        }

        [Header("Name of Remove Ads products")]
        public string removeAds = "Remove_Ads";

        #if EASY_MOBILE
        public static InAppPurchaser Instance { get; private set; }

        void OnEnable()
        {
            IAPManager.PurchaseCompleted += OnPurchaseCompleted;
            IAPManager.RestoreCompleted += OnRestoreCompleted;
        }

        void OnDisable()
        {
            IAPManager.PurchaseCompleted -= OnPurchaseCompleted;
            IAPManager.RestoreCompleted -= OnRestoreCompleted;
        }

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Buy an IAP product using its name
        public void Purchase(string productName)
        {
            if (IAPManager.IsInitialized())
            {
                IAPManager.Purchase(productName);
            }
            else
            {
                MobileNativeUI.Alert("Service Unavailable", "Please check your internet connection.");
            }
        }

        // Restore purchase
        public void RestorePurchase()
        {
            if (IAPManager.IsInitialized())
            {
                IAPManager.RestorePurchases();
            }
            else
            {
                MobileNativeUI.Alert("Service Unavailable", "Please check your internet connection.");
            }
        }

        // Successful purchase handler
        void OnPurchaseCompleted(IAPProduct product)
        {
            string name = product.Name;

            if (name.Equals(removeAds))
            {
                // Purchase of Remove Ads
                AdManager.RemoveAds();
            }
        }

        // Successful purchase restoration handler
        void OnRestoreCompleted()
        {
            MobileNativeUI.Alert("Restore Completed", "Your in-app purchases were restored successfully.");
        }
        #endif
    }
}

