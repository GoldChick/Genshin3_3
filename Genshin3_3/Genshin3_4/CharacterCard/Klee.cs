using TCGBase;
namespace Genshin3_4
{
    public class Klee : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Pyro(1).ToCostInit(),new DamageVariable(3,1)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Pyro(3).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_Klee_E(), c.Index),new DamageVariable(3,3)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Pyro(3).MP(3).ToCostInit(),(skill,me,c,args)=>me.Enemy.AddTeamEffect(new Effect_Klee_Q()),new DamageVariable(3,3))
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Pyro;
        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;
        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Klee_Q : AbstractCardEffect
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
    public class Effect_Klee_E : AbstractCardEffect
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
