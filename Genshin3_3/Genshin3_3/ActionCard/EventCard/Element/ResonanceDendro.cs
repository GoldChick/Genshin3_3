using Minecraft;
using TCGBase;

namespace Genshin3_3
{
    public class ResonanceDendro : AbstractCardEventSingleEffect
    {
        public override CostInit Cost => new CostCreate().Dendro(1).ToCostInit();

        public override string NameID => "element_resonance_dendro";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Geo).Count() >= 2;
        public override int MaxUseTimes => 1;
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var dendro = me.Effects.Find(typeof(DendroCore));
            var catalyze = me.Effects.Find(typeof(CatalyzeField));
            var burning = me.Summons.Find(typeof(Burning));
            if (dendro != null)
            {
                dendro.AvailableTimes++;
            }
            if (catalyze != null)
            {
                catalyze.AvailableTimes++;
            }
            if (burning != null)
            {
                burning.AvailableTimes++;
            }
            base.AfterUseAction(me, targetArgs);
        }
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--},
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID&&v is DamageVariable dv)
                {
                    if (dv.Reaction==ReactionTags.Catalyze||dv.Reaction==ReactionTags.Bloom || dv.Reaction==ReactionTags.Burning)
                    {
                        dv.Damage+=2;
                        p.AvailableTimes--;
                    }
                }
            }
            }
        };
    }
}
