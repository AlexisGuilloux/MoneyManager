using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonthOverviewUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _incomesText;
    [SerializeField] private TextMeshProUGUI _outcomesText;
    [SerializeField] private TextMeshProUGUI _diffText;


    private double _incomes = 0d;
    private double _outcomes = 0d;
    private double _diff = 0d;

    void Start()
    {
        InitMonth();
        Events.OnSelectedMonthChanged += HandleOnSelectedMonthChanged;
    }

    private void OnDestroy()
    {
        Events.OnSelectedMonthChanged -= HandleOnSelectedMonthChanged;
    }


    private void InitMonth()
    {
        _incomes = 0f;
        _outcomes = 0f;
        _diff = 0f;

        var transactions = GameManager.Instance.TransactionManager.GetTransactionsForGivenMonthAndYear();
        for (int i = 0; i < transactions.Count; i++)
        {
            if (transactions[i].entry)
            {
                _incomes += transactions[i].amount;
            }
            else
            {
                _outcomes += transactions[i].amount;
            }
        }

        _diff = _incomes - _outcomes;

        _incomesText.text = $"+{_incomes} €";
        _outcomesText.text = $"-{_outcomes} €";

        _diffText.text = _diff > 0 ? "+" : "-";
        _diffText.text += $"{_diff} €";
    }


    #region Handlers

    private void HandleOnSelectedMonthChanged()
    {
        InitMonth();
    }

    #endregion
}
