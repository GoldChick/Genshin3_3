using TCGBase;

namespace Genshin3_3
{
    public class Dice : AbstractCardEvent
    {
        public override CostInit Cost => new CostInit();

        public override string NameID { get; }
        private readonly int _element;
        public Dice(int element)
        {
            NameID = $"dice_{((ElementCategory)element).ToString().ToLower()}";
            _element = element;
        }

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddDiceRange(_element);
        }
    }
}
