using UnityEngine;

[CreateAssetMenu(fileName = "TransactionCategoryData", menuName = "Custom ScriptableObject/Create new Transaction Category", order = 1)]

public class TransactionCategoryData : ScriptableObject
{
    public string categoryName;
    public TransactionType transactionType;
    public Sprite sprite;
    public bool entry;
}
