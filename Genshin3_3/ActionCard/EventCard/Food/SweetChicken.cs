using TCGBase;

namespace Genshin3_3
{
    public class SweetChicken : SingleCharacterFood
    {
        public override AbstractCardPersistentEffect? AfterEatEffect => null;
        public override int[] Costs => Array.Empty<int>();
        public override string NameID => "food_sweetchicken";
        public override void AfterUseAction(PlayerTeam me, int[]? targetArgs = null)
        {
            me.Heal(this, new DamageVariable(0, 1));
            base.AfterUseAction(me, targetArgs);
        }
    }
}
