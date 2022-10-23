using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //ignore collectibles
        if (other.gameObject.GetComponent<XROrigin>())
        {
            FindObjectOfType<CombatAreaManager>().TriggerCombat();
            GetComponent<Collider>().enabled = false;  //deactivating the trigger so another fight will not start
        }
    }
}
