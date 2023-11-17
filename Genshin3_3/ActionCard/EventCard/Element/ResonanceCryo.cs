using TCGBase;

namespace Genshin3_3
{
    public class ResonanceCryo : AbstractCardEventSingleEffect
    {
        public override int[] Costs => new int[] { 0, 1 };

        public override string NameID => "element_resonance_cryo";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Cryo).Count() >= 2;
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
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
