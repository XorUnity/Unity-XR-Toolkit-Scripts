using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class XrInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForXr());
    }

    IEnumerator CheckForXr()
    {
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            yield return null;

            StartCoroutine(CheckForAvailableXRDevices());
        }
    }

    IEnumerator CheckForAvailableXRDevices()
    {
        var foundXrDevice = false;
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        {
            Debug.Log($"Device found with name '{device.name}' and role '{device.characteristics.ToString()}'");

            foundXrDevice = true;
        }

        yield return null;

        if (!foundXrDevice)
        {
            StopXR();

            // Load NON XR Scene
        }
        else
        {
            // Load XR Scene
        }
    }


    void StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
}
