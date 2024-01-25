using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TransactionOverviewUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _transactionTitle;
    [SerializeField] private TextMeshProUGUI _transactionDate;
    [SerializeField] private TextMeshProUGUI _transactionAmount;
    [SerializeField] private Image _transactionCategoryImage;
    [SerializeField] private Button _modifyTransactionBtn;
    [SerializeField] private Button _deleteTransactionBtn;
    [SerializeField] private Button _exitBtn;

    private TransactionData _transactionData;

    // Start is called before the first frame update
    void Start()
    {
        _modifyTransactionBtn.onClick.AddListener(ModifyTransaction);
        _deleteTransactionBtn.onClick.AddListener(DeleteTransaction);
        _exitBtn.onClick.AddListener(ExitToDefaultScene);

        Init();
    }

    private void OnDestroy()
    {
        _modifyTransactionBtn.onClick.RemoveListener(ModifyTransaction);
        _deleteTransactionBtn.onClick.RemoveListener(DeleteTransaction);
        _exitBtn.onClick.RemoveListener(ExitToDefaultScene);
    }

    private void Init()
    {
        _transactionData = GameManager.Instance.TransactionToDisplay;

        _transactionTitle.text = _transactionData.title;
        _transactionDate.text = _transactionData.date;
        _transactionAmount.text = $"{_transactionData.amount} €";
        _transactionCategoryImage.sprite = Utilities.Converter.GetTransactionCategoryDataFromTransactionType(_transactionData.transactionType).sprite;

        DateTime dataDate = Utilities.Converter.GetDateFromString(_transactionData.date);
        GameManager.SelectedMonth = (Month)dataDate.Month - 1;
        GameManager.SelectedYear = dataDate.Year;
    }

    private void ExitToDefaultScene()
    {
        GameManager.Instance.LoadSceneAsync(SceneName.MainScene);
    }

    private void DeleteTransaction()
    {
        //Add popup to confirm deletion
        Events.OnTransactionDeleted?.Invoke(_transactionData);
        _transactionData = null;
        ExitToDefaultScene();
    }

    private void ModifyTransaction()
    {
        GameManager.Instance.LoadSceneAsync(SceneName.AddTransactionScene);
    }
}
