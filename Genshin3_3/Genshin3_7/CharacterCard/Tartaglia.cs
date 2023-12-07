using TCGBase;

namespace Genshin3_7
{
    public class Tartaglia : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Hydro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Hydro(3).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_Tartaglia_Sword(),c.Index),new DamageVariable(2,2)),
            new Q()
        };
        public override int MaxMP => 3;

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.Fatui;
        private class Q : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Hydro(3).MP(3).ToCostInit();
            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                var far = me.Characters[c.Index].Effects.Find(typeof(Effect_Tartaglia_Sword));
                me.Enemy.Hurt(new(2, far == null ? 5 : 7), this, () =>
                {
                    if (far == null)
                    {
                        me.Enemy.AddPersistent(new Effect_Tartaglia_Stream(), me.Enemy.CurrCharacter);
                        c.MP += 2;
                    }
                });
            }
        }
    }
    public class Effect_Tartaglia_Stream : AbstractCardEffect
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
                        me.AddPersistent(new Effect_Tartaglia_Stream(),me.CurrCharacter);
                    }else
                    {
                        me.AddPersistent(new Effect_Tartaglia_Stream_Die());
                    }
                }
            }
            }
        };
    }
    public class Effect_Tartaglia_Stream_Die : AbstractCardEffect
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterSwitchSender ss)
                {
                    me.AddPersistent(new Effect_Tartaglia_Stream(),ss.Target);
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
    public class Effect_Tartaglia_Sword : AbstractCardEffect
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
                    var a_effect=me.Enemy.Characters[dv.TargetIndex].Effects.Find(typeof(Effect_Tartaglia_Stream));
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
