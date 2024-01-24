using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class CalculatorManager : MonoBehaviour
{
    //All buttons
    [Header("Buttons")]
    //Row 1
    [SerializeField] private Button _ceBtn;
    [SerializeField] private Button _cBtn;
    [SerializeField] private Button _eraseBtn;
    [SerializeField] private Button _divideBtn;

    //Row 2
    [SerializeField] private Button _oneBtn;
    [SerializeField] private Button _twoBtn;
    [SerializeField] private Button _threeBtn;
    [SerializeField] private Button _multiplyBtn;


    //Row 3
    [SerializeField] private Button _fourBtn;
    [SerializeField] private Button _fiveBtn;
    [SerializeField] private Button _sixBtn;
    [SerializeField] private Button _substractBtn;

    //Row 4
    [SerializeField] private Button _sevenBtn;
    [SerializeField] private Button _eightBtn;
    [SerializeField] private Button _nineBtn;
    [SerializeField] private Button _addBtn;

    //Row 5
    [SerializeField] private Button _plusminusBtn;
    [SerializeField] private Button _zeroBtn;
    [SerializeField] private Button _decimalBtn;
    [SerializeField] private Button _equalBtn;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private TextMeshProUGUI _resultText;



    private string _inputString = "";
    private float? _num01;
    private float? _num02;
    private CalculatorOperatorType _operatorType;

    // Start is called before the first frame update
    private void Start()
    {
        //Button subscriptions
        _ceBtn.onClick.AddListener(OnCEBtnPressed);
        _cBtn.onClick.AddListener(OnCBtnPressed);
        _eraseBtn.onClick.AddListener(OnEraseBtnPressed);
        _divideBtn.onClick.AddListener(OnDivideBtnPressed);

        _oneBtn.onClick.AddListener(OnOneBtnPressed);
        _twoBtn.onClick.AddListener(OnTwoBtnPressed);
        _threeBtn.onClick.AddListener(OnThreeBtnPressed);
        _multiplyBtn.onClick.AddListener(OnMultiplyBtnPressed);

        _fourBtn.onClick.AddListener(OnFourBtnPressed);
        _fiveBtn.onClick.AddListener(OnFiveBtnPressed);
        _sixBtn.onClick.AddListener(OnSixBtnPressed);
        _substractBtn.onClick.AddListener(OnSubstractBtnPressed);

        _sevenBtn.onClick.AddListener(OnSevenBtnPressed);
        _eightBtn.onClick.AddListener(OnEightBtnPressed);
        _nineBtn.onClick.AddListener(OnNineBtnPressed);
        _addBtn.onClick.AddListener(OnAddBtnPressed);

        _plusminusBtn.onClick.AddListener(OnPlusMinusBtnPressed);
        _zeroBtn.onClick.AddListener(OnZeroBtnPressed);
        _decimalBtn.onClick.AddListener(OnDecimalBtnPressed);
        _equalBtn.onClick.AddListener(OnEqualBtnPressed);


        Events.OnCalculatorTextChanged += OnCalculatorTextChanged;
        Events.OnTransactionReady += OnTransactionReady;

        _resultText.text = "";
        _inputText.text = "";
    }

    private void OnDestroy()
    {
        //Button unsubscriptions
        _ceBtn.onClick.RemoveListener(OnCEBtnPressed);
        _cBtn.onClick.RemoveListener(OnCBtnPressed);
        _eraseBtn.onClick.RemoveListener(OnEraseBtnPressed);
        _divideBtn.onClick.RemoveListener(OnDivideBtnPressed);

        _oneBtn.onClick.RemoveListener(OnOneBtnPressed);
        _twoBtn.onClick.RemoveListener(OnTwoBtnPressed);
        _threeBtn.onClick.RemoveListener(OnThreeBtnPressed);
        _multiplyBtn.onClick.RemoveListener(OnMultiplyBtnPressed);

        _fourBtn.onClick.RemoveListener(OnFourBtnPressed);
        _fiveBtn.onClick.RemoveListener(OnFiveBtnPressed);
        _sixBtn.onClick.RemoveListener(OnSixBtnPressed);
        _substractBtn.onClick.RemoveListener(OnSubstractBtnPressed);

        _sevenBtn.onClick.RemoveListener(OnSevenBtnPressed);
        _eightBtn.onClick.RemoveListener(OnEightBtnPressed);
        _nineBtn.onClick.RemoveListener(OnNineBtnPressed);
        _addBtn.onClick.RemoveListener(OnAddBtnPressed);

        _plusminusBtn.onClick.RemoveListener(OnPlusMinusBtnPressed);
        _zeroBtn.onClick.RemoveListener(OnZeroBtnPressed);
        _decimalBtn.onClick.RemoveListener(OnDecimalBtnPressed);
        _equalBtn.onClick.RemoveListener(OnEqualBtnPressed);


        Events.OnCalculatorTextChanged -= OnCalculatorTextChanged;
        Events.OnTransactionReady -= OnTransactionReady;
    }

    #region Button Handlers

    private void OnCEBtnPressed()
    {
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnCBtnPressed()
    {

        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnEraseBtnPressed()
    {
        if (string.IsNullOrEmpty(_inputString)) return;

        _inputString = _inputString.Remove(_inputString.Length - 1, 1);
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnDivideBtnPressed()
    {
        ParseInputToFloat();
        _operatorType = CalculatorOperatorType.Divide;

        _inputString += "/";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnOneBtnPressed()
    {
        _inputString += "1";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnTwoBtnPressed()
    {
        _inputString += "2";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnThreeBtnPressed()
    {
        _inputString += "3";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnMultiplyBtnPressed()
    {
        ParseInputToFloat();
        _operatorType = CalculatorOperatorType.Multiply;

        _inputString += "*";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnFourBtnPressed()
    {
        _inputString += "4";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnFiveBtnPressed()
    {
        _inputString += "5";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnSixBtnPressed()
    {
        _inputString += "6";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnSubstractBtnPressed()
    {
        ParseInputToFloat();
        _operatorType = CalculatorOperatorType.Subtract;

        _inputString += "-";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnSevenBtnPressed()
    {
        _inputString += "7";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnEightBtnPressed()
    {
        _inputString += "8";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnNineBtnPressed()
    {
        _inputString += "9";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnAddBtnPressed()
    {
        ParseInputToFloat();
        _operatorType = CalculatorOperatorType.Add;

        _inputString += "+";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnPlusMinusBtnPressed()
    {
        if (_inputString[0] is '-' or '+')
        {
            _inputString.Remove(0, 1);
            _inputString.Insert(0, _inputString[0] is '-' ? "+" : "-");
        }
        else
        {
            _inputString.Insert(0, "-");
        }
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnZeroBtnPressed()
    {
        _inputString += "0";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnDecimalBtnPressed()
    {
        _inputString += ".";
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void OnEqualBtnPressed()
    {
        Events.OnCalculatorTextChanged?.Invoke();
        ParseInputToFloat();
        ProcessOperation();
    }

    #endregion

    #region EventHandlers

    private void OnCalculatorTextChanged()
    {
        _inputText.text = _inputString;
    }

    private void OnCalculatorError()
    {
        _inputString = string.Empty;
        _inputText.text = "";
        _num01 = 0;
        _num02 = 0;
        _resultText.text = "error";
       
        Events.OnCalculatorTextChanged?.Invoke();
    }

    private void ParseInputToFloat()
    {
        //Get rid of any operator sign at the beginning of the string
        if (!char.IsNumber(_inputString[0]))
        {
            _inputString = _inputString.Substring(1);
        }


        //Parse input into float variable, handle error if fail to parse
        if (!float.TryParse(_inputString, NumberStyles.Float, CultureInfo.InvariantCulture, out float r))
        {
            OnCalculatorError();
            return;
        }

        if (_num01 == null)
        {
            _num01 = r;
        }
        else
        {
            _num02 = r;
        }
        _inputString = string.Empty;
    }

    private void ProcessOperation()
    {
        float result = 0;
        switch (_operatorType)
        {
            case CalculatorOperatorType.Add:
                result = _num01.Value + _num02.Value;
                break;
            case CalculatorOperatorType.Subtract:
                result = _num01.Value - _num02.Value;
                break;
            case CalculatorOperatorType.Multiply:
                result = _num01.Value * _num02.Value;
                break;
            case CalculatorOperatorType.Divide:
                result = _num01.Value / _num02.Value;
                break;
            default:
                OnCalculatorError();
                break;
        }
        _resultText.text = result.ToString(CultureInfo.InvariantCulture);
        _num01 = null;
        _num02 = null;

        Events.OnTransactionAmountAdded?.Invoke(result);
    }

    private void OnTransactionReady(bool ready)
    {
        if (ready)
        {
            //var  =_equalBtn.GetComponent<Image>();
        }
        else
        {

        }
    }

    #endregion
}
