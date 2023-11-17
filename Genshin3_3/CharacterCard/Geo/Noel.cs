using TCGBase;

namespace Genshin3_3
{
    public class Noel : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,5),
            new CharacterEffectE(5,1,new Effect_Noel_E(),false),
            new CharacterEffectQ(5,4,new Effect_Noel_Q(),true,5,4)
        };

        public override ElementCategory CharacterElement => ElementCategory.Geo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Noel_E : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.HurtDecrease,new PersistentYellowShield()},
                {
                    SenderTag.HurtMul,(me, p, s, v) =>
                    {
                        if (me.TeamIndex == s.TeamID && v is DamageVariable dv && dv.TargetIndex == me.CurrCharacter)
                        {
                            dv.Damage = (dv.Damage + 1) / 2;
                        }
                    }
                },
            };
    }
    public class Effect_Noel_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {SenderTag.AfterAnyAction,(me,p,s,v)=>p.Data=null},
            {SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=> p.Data==null  && s.ChaIndex==p.PersistentRegion && s.SkillIndex==0
                ,5,1,false,(me,p,s,v)=>p.Data=1
            )},
            {SenderTag.ElementEnchant,new PersistentElementEnchant(5,false,2)},
            {SenderTag.RoundOver,(me,p,s,v)=> {p.AvailableTimes--;  } }
        };
    }

    public class Talent_Noel : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "noel";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 3 };
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver, (me, p, s, v) => p.Data = null},
            { SenderTag.AfterUseSkill, (me, p, s, v) =>
            {
                if (s is AfterUseSkillSender ss && ss.CharIndex==p.PersistentRegion && ss.Skill.Category==SkillCategory.A)
                {
                    if (p.Data==null && me.Effects.Contains(typeof(Effect_Noel_E)) && me.Characters[p.PersistentRegion].Alive)
                    {
                        me.Heal(ss.Skill,new DamageVariable(0,1),new DamageVariable(0,1,0,true));
                        p.Data=1;
                    }
                }
            }
            }
        };
    }

}
