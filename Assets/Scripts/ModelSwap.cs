/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;


public class ModelSwap : MonoBehaviour 
{
	public GameObject userModel;
    private GameObject mDefaultModel;
    private GameObject mExtTrackedModel;
    private GameObject mActiveModel = null;
    private TrackableSettings mTrackableSettings = null;

	void Start () 
    {
		if (!userModel) {
			mDefaultModel = (this.transform.FindChild ("teapot").gameObject) ? this.transform.FindChild ("teapot").gameObject : null;
			mExtTrackedModel = (this.transform.FindChild ("tower").gameObject) ? this.transform.FindChild ("tower").gameObject : null;
			mActiveModel = mDefaultModel;
		} else {
			mActiveModel = userModel;
		}
        mTrackableSettings = FindObjectOfType<TrackableSettings>();
    }
    
    void Update () 
    {
		if (userModel) {
			userModel.SetActive (true);
			mActiveModel = userModel;
		} else {

			if (mTrackableSettings.IsExtendedTrackingEnabled () && (mActiveModel == mDefaultModel)) {
				// Switch 3D model to tower
				mDefaultModel.SetActive (false);
				mExtTrackedModel.SetActive (true);
				mActiveModel = mExtTrackedModel;
			} else if (!mTrackableSettings.IsExtendedTrackingEnabled () && (mActiveModel == mExtTrackedModel)) {
				// Switch 3D model to teapot
				mExtTrackedModel.SetActive (false);
				mDefaultModel.SetActive (true);
				mActiveModel = mDefaultModel;
			}
		}
    }
}
