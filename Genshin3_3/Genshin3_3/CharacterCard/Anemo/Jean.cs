using TCGBase;

namespace Genshin3_3
{
    public class Jean : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,7),
            new E(),
            new Q()
        };

        public override ElementCategory CharacterElement => ElementCategory.Anemo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
        private class E : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.E;

            public override CostInit Cost => new CostCreate().Anemo(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(7, 3), this, me.Enemy.SwitchToNext);
            }
        }
        private class Q : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override CostInit Cost => new CostCreate().Anemo(4).MP(2).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Heal(this, new(2), new(2, 0, true));
                me.AddSummon(new Summon_Jean());
            }
        }
    }
    public class Summon_Jean : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=> me.Enemy.Hurt(new(7,1),this,()=>me.Heal(this,new HealVariable(1)))}
        };
    }
    public class Talent_Jean : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;

        public override string CharacterNameID => "jean";

        public override CostInit Cost => new CostCreate().Anemo(4).MP(2).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.Characters[p.PersistentRegion].Alive && me.TeamIndex==s.TeamID && v is DamageVariable dv  && dv.Element==7)
                {
                    dv.Damage++;
                }
            }
            }
        };
    }
}
