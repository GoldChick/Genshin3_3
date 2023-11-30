using TCGBase;

namespace Genshin3_3
{
    public class Noel : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Geo(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterEffectE(5,1,new Effect_Noel_E(),false),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Geo(4).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Noel_Q(),c.Index),new DamageVariable(5,4)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Geo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Noel_E : AbstractPersistentShieldYellow
    {
        public override int MaxUseTimes => 2;
        public Effect_Noel_E()
        {
            TriggerDic.Add(SenderTag.HurtMul, (me, p, s, v) =>
            {
                if (me.TeamIndex == s.TeamID && v is DamageVariable dv && dv.TargetIndex == me.CurrCharacter)
                {
                    dv.Damage = (dv.Damage + 1) / 2;
                }
            });
        }
    }
    public class Effect_Noel_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {SenderTag.AfterAnyAction,(me,p,s,v)=>p.Data=null},
            {SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=> me.TeamIndex==s.TeamID && p.Data==null  && s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A
                ,5,1,false,(me,p,s)=>p.Data=1
            )},
            {SenderTag.ElementEnchant,new PersistentElementEnchant(5,false,2)},
            {SenderTag.RoundStep,(me,p,s,v)=> p.AvailableTimes-- }
        };
    }

    public class Talent_Noel : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "noel";

        public override int Skill => 1;

        public override CostInit Cost => new CostCreate().Geo(3).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep, (me, p, s, v) => p.Data = null},
            { SenderTag.AfterUseSkill, (me, p, s, v) =>
            {
                if (s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.A)
                {
                    if (p.Data==null && me.Effects.Contains(typeof(Effect_Noel_E)) && me.Characters[p.PersistentRegion].Alive)
                    {
                        me.Heal(ss.Skill,new (1),new (1,0,true));
                        p.Data=1;
                    }
                }
            }
            }
        };
    }

}
