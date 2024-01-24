using System;

[Serializable]
public class TransactionData
{
    public int id;
    public string date;
    public string title;
    public float amount;
    public TransactionType transactionType;
    public bool entry;
}
