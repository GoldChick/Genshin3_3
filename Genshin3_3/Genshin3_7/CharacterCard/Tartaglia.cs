using TCGBase;

namespace Genshin3_7
{
    public class Tartaglia : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,2),
            new CharacterEffectE(2,2,new Effect_Tartaglia_E_Close()),
        };

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.Fatui;
        private class Q : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();
            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                var far = me.Characters[c.Index].Effects.Find(typeof(Effect_Tartaglia_E_Close));
                me.Enemy.Hurt(new(2, far == null ? 5 : 7), this, () =>
                {
                    if (far == null)
                    {
                        me.Enemy.AddPersistent(new Effect_Tartaglia_A(), me.Enemy.CurrCharacter);
                        c.MP += 2;
                    }
                });
            }
        }
    }
    public class Effect_Tartaglia_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => false;

        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.GameStart,(me,p,s,v)=>me.AddPersistent(new Effect_Tartaglia_E_Far(),p.PersistentRegion) },
            { SenderTag.AfterPersistentOtherDesperated,(me,p,s,v)=>
            {
                if (s.TeamID==me.TeamIndex&&s is PersistentDesperatedSender ss && ss.Persistent is Effect_Tartaglia_E_Close && ss.Region==p.PersistentRegion)
                {
                    me.AddPersistent(new Effect_Tartaglia_E_Far(),p.PersistentRegion);
                }
            }
            },
        };
    }
    public class Effect_Tartaglia_A : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.Die,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is DieSender die && die.Cha_Index==p.PersistentRegion)
                {
                    if (me.CurrCharacter!=die.Cha_Index)
                    {
                        me.AddPersistent(new Effect_Tartaglia_A(),me.CurrCharacter);
                    }else
                    {
                        me.AddPersistent(new Effect_Tartaglia_A_Die());
                    }
                }
            }
            }
        };
    }
    public class Effect_Tartaglia_A_Die : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterSwitchSender ss)
                {
                    me.AddPersistent(new Effect_Tartaglia_A(),ss.Target);
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
    public class Effect_Tartaglia_E_Far : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&p.PersistentRegion==s.Character.Index&&s.Skill.Category==SkillCategory.A && me.GetDices().Sum()%2==0,
                0,0,false,(me,p,s)=>p.Data=114)},
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && p.Data!=null && v is DamageVariable dv)
                {
                    me.Enemy.AddPersistent(new Effect_Tartaglia_A(),dv.TargetIndex);
                    p.Data=null;
                }
            }
            }
        };
    }
    public class Effect_Tartaglia_E_Close : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>{p.AvailableTimes--;p.Data=null; } },
            { SenderTag.ElementEnchant,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    dv.Element=2;
                    var a_effect=me.Enemy.Characters[dv.TargetIndex].Effects.Find(typeof(Effect_Tartaglia_A));
                    if (a_effect!=null)
                    {
                        if (p.Data==null)
                        {
                            dv.Damage++;
	                    }else if(1.Equals(p.Data))
                        {

                        }
	                }
                }
            }
            },
        };
    }
    public class Talent_Tartaglia : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "tartaglia";

        public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();
    }
}
