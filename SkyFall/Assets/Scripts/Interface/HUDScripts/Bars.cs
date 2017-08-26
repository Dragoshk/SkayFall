using UnityEngine;
using UnityEngine.UI;
public class Bars : MonoBehaviour
{
    private float _fillAmount;
    [SerializeField]
    private float _lerpSpeed;
    [SerializeField]
    private Image _content;
    [SerializeField]
    private Text _valText;

    public float MaxValue { get; set; }
    public float Value
    {        
        set
        {
            string[] tmp = _valText.text.Split(':');
            _valText.text = tmp[0] + ":" + value;
            _fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
	void Update ()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (_fillAmount != _content.fillAmount)
            _content.fillAmount = Mathf.Lerp(_content.fillAmount,_fillAmount,Time.deltaTime * _lerpSpeed);
    }

    private float Map(float value, float inMin, float inMax, float outMin,float outMax)
    {
        return (value- inMin) * (outMax - outMin)/(inMax - inMin) + outMin;
    }
}
