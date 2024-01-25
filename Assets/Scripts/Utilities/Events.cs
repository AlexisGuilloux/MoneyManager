using System;
using System.Collections.Generic;

public class Events
{
    /// <summary>
    /// Event called when the user selects an other month/year in the UI header.
    /// </summary>
    public static Action OnSelectedMonthChanged;

    /// <summary>
    /// Event called when the user add a character in the calculator.
    /// </summary>
    public static Action OnCalculatorTextChanged;

    #region TransactionEvents

    public static Action<string> OnTransactionTitleAdded;

    public static Action<TransactionCategoryData> OnTransactionCategoryAdded;

    public static Action<float> OnTransactionAmountAdded;

    public static Action<string> OnTransactionDateAdded;

    public static Action<bool> OnTransactionReady;

    public static Action<TransactionData> OnTransactionCreated;
    public static Action<List<TransactionData>> OnTransactionsCreated;

    public static Action<TransactionData> OnTransactionDeleted;
    public static Action<TransactionData> OnTransactionUpdated;



    #endregion

}
