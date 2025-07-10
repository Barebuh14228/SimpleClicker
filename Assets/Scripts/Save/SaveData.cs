using System;
using System.Collections.Generic;
using Components;

namespace Save
{
    [Serializable]
    public class SaveData
    {
        public Balance Balance;
        public List<BusinessSaveData> BusinessEntities;
    }

    [Serializable]
    public class BusinessSaveData
    {
        public Business Business;
        public RevenueProgress RevenueProgress;
        public RevenueModifier RevenueModifier;
        public PurchasedUpgrades PurchasedUpgrades;
    }
}