// using UnityEngine;
// using UnityEngine.UI;
// using AppsFlyerSDK;

// public class ConversionDataUI : MonoBehaviour
// {
//     [SerializeField] private Text _mediaSourceText;
//     [SerializeField] private Text _campaignText;
//     [SerializeField] private Text _adSetText;

//     string conversionData;

//     private void Awake() 
//     {
//         AppsFlyer.initSDK("p6kCY8PcXysqHoQVFRmowj", "com.orbitrush.test.app");
//         AppsFlyer.startSDK();
//     }

//     private void Start() => OnConversionDataReceived();

//     private void OnConversionDataReceived()
//     {
//         AppsFlyer.getConversionData(conversionData);
//         if (conversionData != null)
//         {
//             _mediaSourceText.text = "Media Source: " + conversionData.media_source;
//             _campaignText.text = "Campaign: " + conversionData.campaign;
//             _adSetText.text = "Ad Set: " + conversionData.adset;
//         }
//         else
//         {
//             _mediaSourceText.text = "Error: Conversion data is not available";
//             _campaignText.text = "";
//             _adSetText.text = "";
//         }
//     }
// }