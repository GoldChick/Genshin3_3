using TCGBase;

namespace Genshin3_3
{
    public class SweetChicken : SingleCharacterFood
    {
        public override AbstractCardPersistent? AfterEatEffect => null;
        public override int[] Costs => Array.Empty<int>();
        public override string NameID => "food_sweetchicken";
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Heal(this, new DamageVariable(0, 1, targetArgs[0] - me.CurrCharacter));
            base.AfterUseAction(me, targetArgs);
        }
    }
}
