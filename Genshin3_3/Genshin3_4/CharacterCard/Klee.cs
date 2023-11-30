using TCGBase;
namespace Genshin3_4
{
    public class Klee : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(3,1),
            new CharacterEffectE(3,3,new Effect_Klee_E()),
            new Q()
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Pyro;
        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;
        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
        public override string NameID => "klee";
        private class Q : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;
            public override CostInit Cost => new CostCreate().Pyro(3).MP(2).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                me.Enemy.Hurt(new DamageVariable(3, 3), this, () => me.Enemy.AddPersistent(new Effect_Klee_Q()));
            }
        }
    }
    public class Effect_Klee_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.AfterUseSkill,(me,p,s,v)=>
                    {
                        if (me.TeamIndex==s.TeamID)
                        {
                            me.Hurt(new DamageVariable(3,2),this);
                            p.AvailableTimes--;
                        }
                    }
                }
            };
    }
    public class Effect_Klee_E : AbstractCardPersistent
    {
        public override int MaxUseTimes { get; }
        public Effect_Klee_E(bool talent = false)
        {
            MaxUseTimes = talent ? 2 : 1;
            Variant = talent ? 1 : 0;
        }
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>  me.GetDices().Sum()%2==0 && s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A,
                0,1,false,
                (me,p,s)=> p.Data=1) },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (p.Data!=null  && v is DamageVariable dv)
                {
                    dv.Damage++;
                    p.Data=null;
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
    public class Talent_Klee : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "klee";
        public override int Skill => 1;
        public override CostInit Cost => new CostCreate().Pyro(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new DamageVariable(3, 3), this, () => me.AddPersistent(new Effect_Klee_E(true), c.Index));
        }
    }
}
