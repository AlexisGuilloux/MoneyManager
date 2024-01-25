using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TransactionEditManager : MonoBehaviour
{
    [SerializeField] private Button _previousBtn;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private TextMeshProUGUI _stepTitleText;
    [SerializeField] private GameObject[] _stepScreens;
    [SerializeField] private Button _exitBtn;

    [Space]
    [SerializeField] private TransactionCategoriesDisplayManager _categoriesDisplayManager;
    [SerializeField] private TransactionAddTitleUIManager _addTitleUIManager;
    [SerializeField] private TransactionAddDateUIManager _addDateUIManager;
    [SerializeField] private CalculatorManager _calculatorManager;

    private string _transactionTitle;
    private TransactionCategoryData _transactionCategoryData;
    private float _transactionAmount = float.MinValue;
    private string _transactionDate;
    private int _stepIndex = 0;
    private string[] _stepTitles = new string[] {"Category", "Title", "Date", "Amount" };
    private TransactionData _transactionData;

    public static Action UserConfirmTransactionEvent;

    #region Mono
    private void Start()
    {
        _transactionData = GameManager.Instance.TransactionToDisplay;

        //Set btns
        _previousBtn.onClick.AddListener(OnPreviousBtnPressed);
        _nextBtn.onClick.AddListener(OnNextBtnPressed);
        _exitBtn.gameObject.SetActive(true);
        _exitBtn.onClick.AddListener(OnExitBtnPressed);

        //Event listeners
        Events.OnTransactionTitleAdded += OnTransactionTitleAdded;
        Events.OnTransactionCategoryAdded += OnTransactionCategoryAdded;
        Events.OnTransactionAmountAdded += OnTransactionAmountAdded;
        Events.OnTransactionDateAdded += OnTransactionDateAdded;
        UserConfirmTransactionEvent += UserConfirmTransaction;


        UpdateScreen();
    }

    private void OnDestroy()
    {
        _previousBtn.onClick.RemoveListener(OnPreviousBtnPressed);
        _nextBtn.onClick.RemoveListener(OnNextBtnPressed);


        Events.OnTransactionTitleAdded -= OnTransactionTitleAdded;
        Events.OnTransactionCategoryAdded -= OnTransactionCategoryAdded;
        Events.OnTransactionAmountAdded -= OnTransactionAmountAdded;
        Events.OnTransactionDateAdded -= OnTransactionDateAdded;
        UserConfirmTransactionEvent -= UserConfirmTransaction;
    }
    #endregion

    #region ButtonsBehaviour
    private void OnPreviousBtnPressed()
    {
        if (CheckScreenIndexLimits(false)) return;
        _stepIndex--;
        UpdateScreen();
    }

    private void OnNextBtnPressed()
    {
        if (CheckScreenIndexLimits(true)) return;
        _stepIndex++;
        UpdateScreen();
    }

    private void OnExitBtnPressed()
    {
        /*
        //Create new transaction
        TransactionData newTransaction = new TransactionData()
        {
            id = 0,
            date = _transactionDate,
            title = _transactionTitle,
            amount = _transactionAmount,
            transactionType = _transactionCategoryData.transactionType,
            entry = _transactionCategoryData.entry
        };
        //newTransaction.Init(0, _transactionDate, _transactionTitle, _transactionAmount, _transactionCategoryData.transactionType, _transactionCategoryData.entry);


        //TODO: This will only work on editor, need to find an other way to store the data
        //AssetDatabase.CreateAsset(newTransaction, $"Assets/Resources/ScriptableObjects/Transactions/{newTransaction.title}.asset");
        Events.OnTransactionCreated?.Invoke(newTransaction);
        */

        //Exit to default scene
        GameManager.Instance.LoadSceneAsync(SceneName.MainScene);
    }

    #endregion

    #region Behaviours

    private void UpdateScreen()
    {
        _categoriesDisplayManager.Init(_transactionData);
        _addTitleUIManager.Init(_transactionData);
        _addDateUIManager.Init(_transactionData);
        _calculatorManager.Init(_transactionData);


        for (int i = 0; i < _stepScreens.Length; i++)
        {
            _stepScreens[i].SetActive(false);
        }
        _stepScreens[_stepIndex].SetActive(true);
        _stepTitleText.text = _stepTitles[_stepIndex];


        CheckForValidation();
    }

    private void CheckForValidation()
    {

        if(_transactionAmount == float.MinValue || _transactionCategoryData == null || string.IsNullOrEmpty(_transactionTitle) || string.IsNullOrEmpty(_transactionDate))
        {
            return;
        }
        Events.OnTransactionReady?.Invoke(true);

    }
    private bool CheckScreenIndexLimits(bool increment)
    {
        //Check if out of bound
        if (increment && _stepIndex >= _stepScreens.Length - 1)
        {
            return true;
        }

        if (!increment && _stepIndex <= 0)
        {
            return true;
        }

        //If not, ok for increment/decrement
        return false;
    }

    #endregion

    #region EventHandlers

    private void OnTransactionTitleAdded(string title)
    {
        _transactionTitle = title;
    }

    private void OnTransactionCategoryAdded(TransactionCategoryData data)
    {
        _transactionCategoryData = data;
    }

    private void OnTransactionAmountAdded(float amount)
    {
        _transactionAmount = amount;
        CheckForValidation();
    }

    private void OnTransactionDateAdded(string date)
    {
        _transactionDate = date;
    }

    private void UserConfirmTransaction()
    {
        _transactionData = new TransactionData()
        {
            id = 0,
            date = _transactionDate,
            title = _transactionTitle,
            amount = _transactionAmount,
            transactionType = _transactionCategoryData.transactionType,
            entry = _transactionCategoryData.entry
        };

        if (GameManager.Instance.TransactionToDisplay != null)
        {
            //Update data
            Events.OnTransactionUpdated?.Invoke(_transactionData);
        }
        else
        {
            //Write new data
            Events.OnTransactionCreated?.Invoke(_transactionData);
        }

        OnExitBtnPressed();
    }

    #endregion
}
