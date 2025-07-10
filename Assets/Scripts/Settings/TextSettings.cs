using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "TextSettings", menuName = "Settings/TextSettings")]
    public class TextSettings : ScriptableObject
    {
        [SerializeField] private List<TextKeyValuePair> _textKeyValuePairs;
        
        public Dictionary<string, string> Texts => _textKeyValuePairs.ToDictionary(kv => kv.Key, kv => kv.Value);

        [ContextMenu("Add Texts")]
        public void AddGameTexts()
        {
            _textKeyValuePairs = new List<TextKeyValuePair>()
            {
                new ("business_1","Бизнес №1"),
                new ("business_2","Бизнес №2"),
                new ("business_3","Бизнес №3"),
                new ("business_4","Бизнес №4"),
                new ("business_5","Бизнес №5"),
                new ("upg_1_1","Апгрейд №1"),
                new ("upg_1_2","Апгрейд №2"),
                new ("upg_2_1","Апгрейд №1"),
                new ("upg_2_2","Апгрейд №2"),
                new ("upg_3_1","Апгрейд №1"),
                new ("upg_3_2","Апгрейд №2"),
                new ("upg_4_1","Апгрейд №1"),
                new ("upg_4_2","Апгрейд №2"),
                new ("upg_5_1","Апгрейд №1"),
                new ("upg_5_2","Апгрейд №2"),
                new ("price_field","Цена: {0} $"),
                new ("revenue_mod_field","Доход: + {0} %"),
                new ("purchased","КУПЛЕНО !"),
                new ("quote_text","\"{0}\""),
                new ("money_field","{0} $")
            };
        }
        
    }

    [Serializable]
    public class TextKeyValuePair
    {
        [SerializeField] private string _key;
        [SerializeField] private string _value;
        
        public string Key => _key;
        public string Value => _value;

        public TextKeyValuePair(string key, string value)
        {
            _key = key;
            _value = value;
        }
    }
}