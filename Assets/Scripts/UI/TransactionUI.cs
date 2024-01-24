using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransactionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _transactionTypeText;

    public void Init(TransactionData data)
    {
        _titleText.text = data.title;
        _amountText.text = data.entry ? "+" : "-";
        _amountText.text += $"{data.amount} €";
        _transactionTypeText.sprite = Utilities.Converter.GetTransactionCategoryDataFromTransactionType(data.transactionType).sprite;
    }
}
