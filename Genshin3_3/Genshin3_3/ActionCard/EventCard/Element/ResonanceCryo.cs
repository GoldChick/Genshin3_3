using TCGBase;

namespace Genshin3_3
{
    public class ResonanceCryo : AbstractCardEventSingleEffect
    {
        public override CostInit Cost => new CostCreate().Cryo(1).ToCostInit();

        public override string NameID => "element_resonance_cryo";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Cryo).Count() >= 2;
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    dv.Damage+=2;
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}
