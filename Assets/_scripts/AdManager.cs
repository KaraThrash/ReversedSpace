using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour,IUnityAdsListener
{
  public string gameId = "1234567";
  public string placementId = "bannerPlacement";
  public bool testMode = true;
  public Button myButton;
   public string myPlacementId = "rewardedVideo";
  void Start () {

    // myButton = GetComponent <Button> ();

      // Set interactivity to be dependent on the Placement’s status:
      myButton.interactable = Advertisement.IsReady (myPlacementId);

      // Map the ShowRewardedVideo function to the button’s click listener:
      if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);

      // Initialize the Ads listener and service:
      Advertisement.AddListener (this);
      Advertisement.Initialize (gameId, true);

        StartCoroutine (ShowBannerWhenReady ());
  }
  void ShowRewardedVideo () {
        Advertisement.Show (myPlacementId);
    }
  IEnumerator ShowBannerWhenReady () {
      while (!Advertisement.IsReady ()) {
          yield return new WaitForSeconds (0.5f);
      }
        // Advertisement.Show ();
      Advertisement.Banner.Show (placementId);
  }
  public void OnUnityAdsReady (string placementId) {
      // If the ready Placement is rewarded, activate the button:
      if (placementId == myPlacementId) {
          myButton.interactable = true;
      }
  }
  public void OnUnityAdsDidError (string message) {
       // Log the error.
        Debug.LogWarning ("errored");
   }

   public void OnUnityAdsDidStart (string placementId) {
       // Optional actions to take when the end-users triggers an ad.
       Debug.LogWarning ("started");
   }
  public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            // Reward the user for watching the ad to completion.
            Debug.LogWarning ("watched");
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
              Debug.LogWarning ("skip");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }
}
