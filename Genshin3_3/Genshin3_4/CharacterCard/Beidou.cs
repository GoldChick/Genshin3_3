using TCGBase;

namespace Genshin3_4
{
    public class Beidou : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Electro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterEffectE(new Effect_Beidou_E()),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Electro(3).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Beidou_Q())
                ,new DamageVariable(4,2)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "beidou";

    }
    public class Effect_Beidou_E : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterSwitchSender ss && ss.Target!=p.PersistentRegion)
                {
                    p.Active=false;
                }
            }
            },
            { SenderTag.RoundMeStart,(me,p,s,v)=>
                {
                    var c=me.Characters[p.PersistentRegion];
                    if (c.Active)
                    {
                        me.Enemy.Hurt(new DamageVariable(4, 3), c.Card.Skills[1]);
                        p.Active=false;
                        //TODO:战斗行动？
                    }
                }
            }
        };
    }
    public class Effect_Beidou_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>me.Enemy.Hurt(new(4,1),this) },
            { SenderTag.HurtDecrease,new PersistentPurpleShield(1,3)}
        };
    }
    public class Effect_Beidou_Talent : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.Active=false},
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>((me,p,s,v)=>
            s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A
            ,-1,1)}
        };
    }
    public class Talent_Beidou : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "beidou";
        public override int Skill => 1;
        public override CostInit Cost => new CostCreate().Electro(3).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (s is AfterUseSkillSender uss  && p.AvailableTimes>0 && uss.Character.Index==p.PersistentRegion && uss.Skill.Category==SkillCategory.E)
                {
                    me.AddPersistent(new Effect_Beidou_Talent(), p.PersistentRegion);
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}
