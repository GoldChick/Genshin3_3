using TCGBase;
namespace Genshin3_3
{
    public class Sucrose : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Anemo(1).ToCostInit(),new DamageVariable(7,1)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Anemo(3).ToCostInit(),(skill,me,c,args)=>me.Enemy.SwitchToLast(),new DamageVariable(7,3)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Anemo(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Sucrose()),new DamageVariable(7,1))
        };

        public override string NameID => "sucrose";

        public override ElementCategory CharacterElement => ElementCategory.Anemo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

    }
    public class Summon_Sucrose : AbstractColorfulSummon
    {
        public Summon_Sucrose(bool talent = false) : base(2, 3)
        {
            Variant = talent ? 1 : 0;
            if (talent)
            {
                TriggerDic.Add(SenderTag.DamageIncrease, (me, p, s, v) =>
                {
                    if (p.Data is int element && me.TeamIndex == s.TeamID && v is DamageVariable dv && dv.Element == element)
                    {
                        dv.Damage++;
                    }
                });
            }
        }

        public override int MaxUseTimes => 3;
    }
    public class Talent_Sucrose : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;

        public override string CharacterNameID => "sucrose";

        public override CostInit Cost => new CostCreate().Anemo(3).MP(2).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(7, 1), c.Card.Skills[2], () => me.AddSummon(new Summon_Sucrose(true)));
        }
    }
}
