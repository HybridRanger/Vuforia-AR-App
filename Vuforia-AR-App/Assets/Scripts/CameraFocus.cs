using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool focusModeSet = Vuforia.CameraDevice.Instance.SetFocusMode(
        Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        if (!focusModeSet)
        {
            Debug.Log("Failed to set focus mode (unsupported mode).");
        }
    }
}