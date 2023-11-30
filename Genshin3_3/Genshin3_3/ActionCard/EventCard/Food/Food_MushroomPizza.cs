using TCGBase;

namespace Genshin3_3
{
    public class Food_MushroomPizza : AbstractCardFoodSingle
    {
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>{me.Heal(this, new HealVariable(1,p.PersistentRegion-me.CurrCharacter));p.AvailableTimes--; } }
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Heal(this, new HealVariable(1, targetArgs[0] - me.CurrCharacter));
            base.AfterUseAction(me, targetArgs);
        }
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs)
        {
            var c = me.Characters[targetArgs[0]];
            return c.Alive && c.HP < c.Card.MaxHP && !c.Effects.Contains("minecraft", "full");
        }
    }
}
