using TCGBase;

namespace Genshin3_3
{
    public class Mona : AbstractCardCharacter
    {
        public override int MaxMP => 3;

        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Hydro(1).ToCostInit(),new DamageVariable(2,1)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Hydro(3).ToCostInit(),(skill,me,c,args)=>me.AddSummon(new Summon_Mona()),new DamageVariable(2,1)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Hydro(3).MP(3).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Mona()),new DamageVariable(2,4)),
            new Effect_Mona_Passive()
        };

        public override string NameID => "mona";

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

    }
    public class Effect_Mona_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => false;

        public override int MaxUseTimes => 1;

        public override bool CustomDesperated => true;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0&&me.TeamIndex==s.TeamID && v is FastActionVariable fv && !fv.Fast && s is AfterSwitchSender ss && ss.Source==p.PersistentRegion)
                {
                    fv.Fast = true;
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
    public class Summon_Mona : AbstractCardSummon
    {
        public override bool CustomDesperated => true;
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.HurtDecrease, new PersistentPurpleShield(1) },
            { SenderTag.RoundOver, (me,p,s,v)=>me.Enemy.Hurt(new(2, 1,  0),this,()=>p.Active=false)}
        };
        public override string NameID => "summon_mona";
    }
    public class Effect_Mona : AbstractCardEffect
    {
        public override string NameID => "effect_mona";
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.HurtMul,(me,p,s,v)=>
                    {
                        if (me.TeamIndex==s.TeamID && v is DamageVariable dv)
                        {
                            dv.Damage *= 2;
                            p.AvailableTimes--;
                        }
                    }
                }
            };
    }
    public class Talent_Mona : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;

        public override string CharacterNameID => "mona";
        public override CostInit Cost => new CostCreate().Hydro(3).MP(3).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID &&me.CurrCharacter==p.PersistentRegion && v is DamageVariable dv && s is PreHurtSender hs)
                {
                     if (dv.Reaction==ReactionTags.Vaporize||dv.Reaction==ReactionTags.Bloom||dv.Reaction==ReactionTags.Frozen ||dv.Reaction==ReactionTags.ElectroCharged ||((dv.Reaction==ReactionTags.Swirl||dv.Reaction==ReactionTags.Crystallize)&& hs.InitialElement==2))
                    {
                        dv.Damage+=2;
                    }
                }
            } }
        };
    }
}
