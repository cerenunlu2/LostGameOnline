using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using Mirror;

public class IAPManager : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        try
        {
            var result = validator.Validate(product.receipt);
            foreach (IPurchaseReceipt productReceipt in result)
            {
                if (productReceipt is GooglePlayReceipt google)
                {
                    GameManager.gm.BenimGemim.GetComponent<Player>().PaketYukle("" + google.transactionID, "" + productReceipt.purchaseDate, product.definition.id);
                }
                else if (productReceipt is AppleInAppPurchaseReceipt apple)
                {
                    // apple kýsmý
                }
            }
        }
        catch (IAPSecurityException hataSorunuyazisi)
        {
            // iþlem baþarýsýz ise
            GameManager.gm.BenimGemim.GetComponent<Player>().OdemeSorunuMobil("IAPSecurityException = " + hataSorunuyazisi.ToString());
        }
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //ödeme baþarýsýz ise
        GameManager.gm.BenimGemim.GetComponent<Player>().OdemeSorunuMobil("failureReason = " + failureReason.ToString());
    }
}