using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button MyButton;
    private bool _isButtonClicked = false;

    void Start()
    {
        MyButton = this.gameObject.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isButtonClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonClicked = false;
    }

    public bool IsButtonClicked()
    {
        return _isButtonClicked;
    }
}
