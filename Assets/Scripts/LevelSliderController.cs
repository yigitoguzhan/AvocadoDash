using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliderController : MonoBehaviour
{
    [SerializeField] Transform endPoint;
    [SerializeField] Transform playerPos;
    [SerializeField] Image levelSlider;
    [SerializeField] float totalDistance;
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = endPoint.position.x - playerPos.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSliderFillAmount();
    }

    public void CalculateSliderFillAmount()
    {
        levelSlider.fillAmount = playerPos.position.x / totalDistance;
    }
}
