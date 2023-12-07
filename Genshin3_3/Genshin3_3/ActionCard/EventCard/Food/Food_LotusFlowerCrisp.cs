using TCGBase;

namespace Genshin3_3
{
    public class Food_LotusFlowerCrisp : AbstractCardFoodSingle
    {
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.HurtDecrease,new PersistentPurpleShield(3,1) },
            new PersistentPreset.RoundStepDecrease(),
        };
    }
}
