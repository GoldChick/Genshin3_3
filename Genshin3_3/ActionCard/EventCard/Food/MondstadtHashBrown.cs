using TCGBase;

namespace Genshin3_3
{
    public class MondstadtHashBrown : SingleCharacterFood
    {
        public override AbstractCardPersistentEffect? AfterEatEffect => null;
        
        public override int[] Costs => Array.Empty<int>();

        public override string NameID => "food_mondstadthashbrown";
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Heal(this, new DamageVariable(0, 2));
            base.AfterUseAction(me, targetArgs);
        }
    }
}
