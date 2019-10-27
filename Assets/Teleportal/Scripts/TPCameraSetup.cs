// Teleportal SDK
// Code by Thomas Suarez
// Copyright 2019 WiTag Inc DBA Teleportal

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

namespace Teleportal {
    
    public class TPCameraSetup : MonoBehaviour
    {
        private TrackedPoseDriver driver;

        void Awake() {
            this.driver = this.gameObject.GetComponent<TrackedPoseDriver>();

            // The color camera of a mobile device
            this.driver.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRDevice, TrackedPoseDriver.TrackedPose.ColorCamera);
        }

        void Start() {
            if (!TPDeviceInfo.IsAugmented) {
                this.driver.enabled = false;
            }
        }
    }

}