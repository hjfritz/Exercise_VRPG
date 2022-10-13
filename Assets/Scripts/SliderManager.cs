using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    [SerializeField] private Slider _slider;

    [SerializeField] private PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
      _slider.onValueChanged.AddListener((v) =>
      {
          pm.difficulty = (int)v;
      });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
