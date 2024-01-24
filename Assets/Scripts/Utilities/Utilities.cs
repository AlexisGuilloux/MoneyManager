
using System;
using UnityEngine;

namespace Utilities
{
    public static class Converter
    {
        public static TransactionCategoryData GetTransactionCategoryDataFromTransactionType(TransactionType type)
        {
            TransactionCategoryData[] datas = GameManager.Instance.TransactionManager.GetTransactionCategoryDatas();

            for (int i = 0; i < datas.Length; i++)
            {
                if (datas[i].transactionType == type)
                {
                    return datas[i];
                }
            }
            return null;
        }

        public static DateTime GetDateFromString(string date)
        {
            return DateTime.Parse(date).Date;
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
