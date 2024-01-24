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
        var data = GameManager.Instance.TransactionToDisplay;

        _transactionTitle.text = data.title;
        _transactionDate.text = data.date;
        _transactionAmount.text = $"{data.amount} €";
        _transactionCategoryImage.sprite = Utilities.Converter.GetTransactionCategoryDataFromTransactionType(data.transactionType).sprite;

        DateTime dataDate = Utilities.Converter.GetDateFromString(data.date);
        GameManager.SelectedMonth = (Month)dataDate.Month - 1;
        GameManager.SelectedYear = dataDate.Year;
    }

    private void ExitToDefaultScene()
    {
        GameManager.Instance.LoadSceneAsync(SceneName.MainScene);
    }

    private void DeleteTransaction()
    {
        
    }

    private void ModifyTransaction()
    {

    }
}
