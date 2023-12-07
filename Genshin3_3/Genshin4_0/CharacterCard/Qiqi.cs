using TCGBase;

namespace Genshin4_0
{
    public class Qiqi : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
        new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Cryo(1).Void(2).ToCostInit(),new DamageVariable(0,2)),
        new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Cryo(3).ToCostInit()),
        new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(3).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_Qiqi()),new DamageVariable(1,3)),
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
    }
    public class Summon_Qiqi : AbstractCardSummon
    {
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me, p, s, v) =>  me.Enemy.Hurt(new DamageVariable(1, 1, 0), this,()=>p.AvailableTimes --) },
            new PersistentPreset.AfterUseSkill((me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s.Character.Card is Qiqi && s.Skill.Category==SkillCategory.A)
                {
                    me.Heal(this,new HealVariable(1,me.FindHPLostMost()-me.CurrCharacter));
                }
            })
        };
    }

    public class Effect_Qiqi : AbstractCardEffect
    {
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.AfterUseSkill((me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID)
                {
                    var c=me.Characters[s.Character.Index];
                    if (c.HP<c.Card.MaxHP)
                    {
                        me.Heal(this,new HealVariable(2,c.Index-me.CurrCharacter));
                        p.AvailableTimes--;
                    }
                }
            })
        };
    }
}
