using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class reklam : MonoBehaviour
{
    public GameObject ayarlar;
    public GameObject anamenupanel;

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-1664062912484400/5142159815";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif

    private InterstitialAd _interstitialAd;


    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        InvokeRepeating("reklamyukle",1f,10f);
    }

    void reklamyukle(){LoadLoadInterstitialAd();}

    
    public void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
            ayarlar.SetActive(true);
            anamenupanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            ayarlar.SetActive(true);
            anamenupanel.SetActive(false);
        }
    }

}
