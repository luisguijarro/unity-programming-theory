using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int maxValue = 100;
    [SerializeField] private int minValue = 0;
    [SerializeField] private int i_value;
    [SerializeField] private GameObject barFilling;
    [SerializeField] private GameObject barBorder;
    [SerializeField] private Color borderColor = Color.white;
    [SerializeField] private Color fillColor = Color.green;

    
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<UnityEngine.UI.>
        UpdateView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ABSTRACTION
    public void SetMaxValue(int maxvalue)
    {
        if (maxvalue > this.minValue)
        {
            this.maxValue = maxvalue;
        }
    }

    // ABSTRACTION
    public void SetMinValue(int minvalue)
    {
        if (minvalue < this.maxValue)
        {
            this.minValue = minvalue;
        }
    }

    // ABSTRACTION
    private void UpdateView()
    {
        barFilling.GetComponent<Image>().color = fillColor;
        this.barBorder.GetComponent<Image>().color = borderColor;

        float RV = this.maxValue - this.minValue;
        float GR = this.GetComponent<RectTransform>().rect.height-5;
        float actualHeight = GR - ((GR/RV)*this.i_value);
        barFilling.GetComponent<RectTransform>().offsetMax = new Vector2(-2.5f, -(2.5f+actualHeight));
    }

    // ENCAPSULATION
    public int Value
    {
        get { return this.i_value; }
        set 
        { 
            if ( (value <= this.maxValue) && (value >= this.minValue))
            {
                this.i_value = value;
                this.UpdateView();
            }            
        }
    }
}
