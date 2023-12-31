﻿using TCGBase;

namespace Genshin3_7
{
    public class HuTao : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Pyro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Pyro(2).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_HuTao_E(), c.Index)),
            new Q()
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
        public override int MaxMP => 3;
        private class Q : AbstractCardSkill
        {
            public override CostInit Cost =>new CostCreate().Pyro(3).MP(3).ToCostInit();

            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(3,c.HP<7?5:4),this,()=>me.Heal(this,new HealVariable(c.HP<7?3:2,c.Index-me.CurrCharacter)));
            }
        }
    }
    public class Effect_HuTao_A : AbstractCardEffect
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Hurt(new(3,1,p.PersistentRegion-me.CurrCharacter),this,()=>p.AvailableTimes--)}
        };
    }
    public class Effect_HuTao_E : AbstractCardEffect
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.ElementEnchant,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    dv.Element=3;
                    dv.Damage++;
                    if (p.Data!=null)
                    {
                        p.Data=null;
                        me.Enemy.AddPersistent(new Effect_HuTao_A(),dv.TargetIndex);
	                }
	            }
            } 
            },
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A && me.GetDices().Sum()%2==0,
                0,0,false,(me,p,s)=>p.Data=114) 
            }
        };
    }
    public class Talent_HuTao : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "hutao";

        public override CostInit Cost => new CostCreate().Pyro(2).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv) && dv.Element==3 && me.Characters[p.PersistentRegion].HP<=6)
                {
                    dv.Damage++;
	            }   
            } 
            }
        };
    }
}
