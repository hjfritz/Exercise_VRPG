using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private InputActionReference MenuActionReference;
    private Transform panel;

    // Start is called before the first frame update
    void Start()
    {
        MenuActionReference.action.performed += OnMenu;
    }

    private void OnMenu(InputAction.CallbackContext obj)
    {
        panel = transform.GetChild(0);
        if(panel.gameObject.activeInHierarchy) panel.gameObject.SetActive(false);
        else
        {
            panel.gameObject.SetActive(true);
        }
    }
}
