using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider teleportationProvider;
    private InputAction _thumbstick;
    private bool _isActive;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        
        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        _thumbstick.Enable();
    }

    private void OnTeleportActivate( InputAction.CallbackContext obj ) {
        rayInteractor.enabled = true;
        _isActive = true;
    }
    private void OnTeleportCancel( InputAction.CallbackContext obj ) {
        rayInteractor.enabled = false;
        _isActive = false;
    }

    // Update is called once per frame
    void Update() {
        if (!_isActive) {
            return;
        }

        if (_thumbstick.triggered) {
            return;
        }

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit)) {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new() {
            destinationPosition = hit.point
        };

        teleportationProvider.QueueTeleportRequest(request);
    }
}
