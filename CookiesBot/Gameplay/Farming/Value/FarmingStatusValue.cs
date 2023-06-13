namespace CookiesBot.Gameplay
{
    public sealed class FarmingStatusValue : IFarmingStatusValue
    {
        private FarmingStatus _value;

        public FarmingStatusValue(FarmingStatus value) 
            => _value = value;

        public FarmingStatusValue()
            => _value = FarmingStatus.Disabled;

        public void Set(FarmingStatus value)
            => _value = value;

        public FarmingStatus Get()
            => _value;
    }
}