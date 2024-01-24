using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransactionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _transactionTypeText;
    [SerializeField] private Button _transactionBtn;

    private TransactionData _transactionData;

    public void Init(TransactionData data)
    {
        _transactionData = data;
        _transactionBtn.onClick.AddListener(OnTransactionBtnPressed);

        _titleText.text = data.title;
        _amountText.text = data.entry ? "+" : "-";
        _amountText.text += $"{data.amount} €";
        _transactionTypeText.sprite = Utilities.Converter.GetTransactionCategoryDataFromTransactionType(data.transactionType).sprite;
    }

    private void OnDestroy()
    {
        _transactionBtn.onClick.RemoveListener(OnTransactionBtnPressed);
    }

    private void OnTransactionBtnPressed()
    {
        GameManager.Instance.TransactionToDisplay = _transactionData;
        GameManager.Instance.LoadSceneAsync(SceneName.TransactionOverviewScene);
    }
}
