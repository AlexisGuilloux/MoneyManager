using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DayGroupUI : MonoBehaviour
{
    [SerializeField] private Transform _dayDetailsTransform;
    [SerializeField] private TextMeshProUGUI _dateText;
    public Transform DayDetailsTransform { get { return _dayDetailsTransform; } }
    public DateTime dayDate { get => _dayDate; set => _dayDate = value; }
    private DateTime _dayDate;

    public void Init(TransactionData data)
    {
        DateTime.TryParse(data.date, out _dayDate);
        _dateText.text = _dayDate.Date.ToString("d");
    }
}