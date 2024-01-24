#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class AddBulkTransactionTool : MonoBehaviour
{
    private int _startingYear = 2022;
    private int _numberOfYearsToAdd = 1;
    private int _minNbreTransactionsPerMonth = 25;
    private int _maxNbreTransactionsPerMonth = 100;
    private float _minTransactionAmount = 1f;
    private float _maxTransactionAmount = 150f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddTransactions();
        }
    }


    private void AddTransactions()
    {
        List<TransactionData> transactionDatas = new();
        for (int i = 0; i < _numberOfYearsToAdd; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                var rand = new Unity.Mathematics.Random();
                rand.InitState();
                int numberOfTransactionToAdd = rand.NextInt(_minNbreTransactionsPerMonth, _maxNbreTransactionsPerMonth);

                for (int k = 0; k < numberOfTransactionToAdd; k++)
                {
                    int randomDay = rand.NextInt(1, DateTime.DaysInMonth(_startingYear + i, j + 1));

                    var categoryData = Utilities.Converter.GetTransactionCategoryDataFromTransactionType((TransactionType)rand.NextInt(0, Enum.GetValues(typeof(TransactionType)).Length - 1));

                    TransactionData newTransaction = new TransactionData()
                    {
                        id = 0,
                        date = new DateTime(_startingYear + i, j + 1, randomDay).ToString(),
                        title = $"{_startingYear + i}-{j + 1} {categoryData.categoryName} {k}",
                        amount = rand.NextFloat(_minTransactionAmount, _maxTransactionAmount),
                        transactionType = categoryData.transactionType,
                        entry = categoryData.entry
                    };
                    transactionDatas.Add(newTransaction);
                }
            }
        }

        Events.OnTransactionsCreated?.Invoke(transactionDatas);
    }
}
#endif