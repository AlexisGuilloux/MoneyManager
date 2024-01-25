using System.Collections.Generic;
using UnityEngine;

public class TransactionManager : MonoBehaviour
{
    private TransactionYear[] _transactionYears;
    private List<TransactionData> _transactionDatas = new();
    private TransactionCategoryData[] _transactionCategoryDatas;


    #region Mono

    private void Start()
    {
        Events.OnTransactionCreated += OnTransactionCreated;
        Events.OnTransactionsCreated += OnTransactionCreated;
        Events.OnTransactionDeleted += OnTransactionDeleted;
        Events.OnTransactionUpdated += OnTransactionUpdated;

        LoadDatas();
    }

    private void OnDestroy()
    {
        Events.OnTransactionCreated -= OnTransactionCreated;
        Events.OnTransactionsCreated -= OnTransactionCreated;
        Events.OnTransactionDeleted -= OnTransactionDeleted;
        Events.OnTransactionUpdated -= OnTransactionUpdated;
    }

    #endregion


    public void LoadDatas()
    {
        var array = Resources.LoadAll("ScriptableObjects/TransactionCategories", typeof(TransactionCategoryData));
        _transactionCategoryDatas = new TransactionCategoryData[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            _transactionCategoryDatas[i] = (TransactionCategoryData)array[i];
        }

        //Import Json to get transactionYears
        _transactionYears = JsonManager.ImportJson();

        _transactionDatas.Clear();
        for (int i = 0; i < _transactionYears.Length; i++)
        {
            for (int j = 0; j < _transactionYears[i].data.Length; j++)
            {
                _transactionDatas.Add(_transactionYears[i].data[j]);
            }
        }
    }
    

    public List<TransactionData> GetTransactionsForGivenMonthAndYear()
    {
        List<TransactionData> transactions = new();

        for (int i = 0; i < _transactionDatas.Count; i++)
        {
            var date = Utilities.Converter.GetDateFromString(_transactionDatas[i].date).Date;
            if (date.Year != GameManager.SelectedYear || date.Month != (int)GameManager.SelectedMonth + 1) continue;

            transactions.Add(_transactionDatas[i]);
        }
        return transactions;
    }

    public TransactionCategoryData[] GetTransactionCategoryDatas()
    {
        return _transactionCategoryDatas;
    }


    private int CheckAndCreateTransactionYearNeeded(int year)
    {
        for (int i = 0; i < _transactionYears.Length; i++)
        {
            if (_transactionYears[i].year != year) continue;
            //return if this find the correct transactionYear object
            return i;
        }
        //If not, a new transactionYear is created and added to the array
        TransactionYear[] newTransactionYears = new TransactionYear[_transactionYears.Length + 1];
        for (int i = 0; i < _transactionYears.Length; i++)
        {
            newTransactionYears[i] = _transactionYears[i];
        }
        newTransactionYears[newTransactionYears.Length - 1] = new TransactionYear { year = year, data = new TransactionData[0] };
        _transactionYears = newTransactionYears;

        return _transactionYears.Length - 1;
    }

    #region EventHandlers

    private void OnTransactionCreated(TransactionData data)
    {
        //Add the data to the transactionDatas list containing all transactions
        _transactionDatas.Add(data);

        //Filter and add the data to the specific TransactionYear object
        //By doing so, I need to do this Array to List conversion, add the data to the tmp list and then List to Array conversion
        //All transationDatas need to be stored as a array for serialization/json conversion within all TransactionYear object
        int dataYear = Utilities.Converter.GetDateFromString(data.date).Year;

        int yearIndex = CheckAndCreateTransactionYearNeeded(dataYear);

        List<TransactionData> transactionDataTmpList = new();
        transactionDataTmpList.AddRange(_transactionYears[yearIndex].data);
        transactionDataTmpList.Add(data);
        _transactionYears[yearIndex].data = transactionDataTmpList.ToArray();
        JsonManager.ExportJson(_transactionYears);
    }

    /// <summary>
    /// Method used to add a batch of transactions with the AddBulkTransactionTool to debug and stress test the app
    /// </summary>
    /// <param name="datas"></param>
    private void OnTransactionCreated(List<TransactionData> datas)
    {
        //Add the data to the transactionDatas list containing all transactions
        for (int i = 0; i < datas.Count; i++)
        {
            _transactionDatas.Add(datas[i]);
            int dataYear = Utilities.Converter.GetDateFromString(datas[i].date).Year;
            int yearIndex = CheckAndCreateTransactionYearNeeded(dataYear);
            List<TransactionData> transactionDataTmpList = new();
            transactionDataTmpList.AddRange(_transactionYears[yearIndex].data);
            transactionDataTmpList.Add(datas[i]);
            _transactionYears[yearIndex].data = transactionDataTmpList.ToArray();
        }

        JsonManager.ExportJson(_transactionYears);
    }

    private void OnTransactionDeleted(TransactionData data)
    {
        if(GameManager.Instance.TransactionToDisplay == data)
        {
            GameManager.Instance.TransactionToDisplay = null;
        }

        for (int i = 0; i < _transactionDatas.Count; i++)
        {
            if (_transactionDatas[i] != data) continue;

            _transactionDatas.RemoveAt(i);
            break;
        }
    }

    private void OnTransactionUpdated(TransactionData data)
    {
        for (int i = 0; i < _transactionDatas.Count; i++)
        {
            if (_transactionDatas[i] != data) continue;
            _transactionDatas[i] = data;
            break;
        }
    }

    #endregion
}
