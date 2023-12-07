using Genshin3_5;
using TCGBase;

namespace Genshin3_7
{
    public class Artifact_EmblemOfSeveredFate : Artifact_OrnateKabuto
    {
        public override int MaxUseTimes => 1;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public Artifact_EmblemOfSeveredFate() : base()
        {
            TriggerDic.Add(SenderTag.DamageIncrease, (me, p, s, v) =>
            {
                if (p.AvailableTimes > 0 && PersistentFunc.IsCurrCharacterSkill(me, p, s, v, SkillCategory.Q, out var dv))
                {
                    dv.Damage += 2;
                    p.AvailableTimes = 0;
                }
            });
            TriggerDic.Add(new PersistentPreset.RoundStepReset());
        }
    }
}
