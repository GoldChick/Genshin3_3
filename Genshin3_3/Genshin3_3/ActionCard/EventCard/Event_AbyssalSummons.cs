using TCGBase;


namespace Genshin3_3
{
    public class Event_AbyssalSummons : AbstractCardEvent
    {
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public static AbstractCardSummon[] Pool => new AbstractCardSummon[]
        {
            new Summon_Hili(ElementCategory.Cryo),
            new Summon_Hili(ElementCategory.Hydro),
            new Summon_Hili(ElementCategory.Pyro),
            new Summon_Hili(ElementCategory.Electro),
        };
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs) => me.AddSummon(1, Pool);
        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterCategory == CharacterCategory.Mob).Count() >= 2;
    }
    public class Summon_Hili : AbstractCardSummon
    {
        public override string NameID { get; }
        public Summon_Hili(ElementCategory element)
        {
            NameID = $"summon_hili_{element.ToString().ToLower()}";
            Variant = (int)element;
        }
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(Variant,1),this,()=>p.AvailableTimes--)}
        };
    }
}
