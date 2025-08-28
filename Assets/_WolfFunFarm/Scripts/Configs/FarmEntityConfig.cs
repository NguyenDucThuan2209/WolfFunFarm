namespace WolfFunFarm
{
    [System.Serializable]
    public class FarmEntityConfig
    {
        public string Id;
        public string Name;
        public int SeedPrice;
        public int SellPrice;
        public int BundleSize;
        public int CycleDuration;
        public int ProductPerCycle;
        public int LifetimeProducts;
    }
}