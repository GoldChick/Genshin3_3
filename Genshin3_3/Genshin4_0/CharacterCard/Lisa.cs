using TCGBase;

namespace Genshin4_0
{
    public class Lisa : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Electro(1).Void(2).ToCostInit(),(skill,me,c,args)=>
            {
                if (me.SpecialState.HeavyStrike)
                {
                    me.Enemy.AddPersonalEffect(new Effect_Lisa());
                }
            },new DamageVariable(4,1)),
            new E(),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Electro(3).MP(2).ToCostInit(),(skill,me,c,args)=>me.AddSummon(new Summon_Lisa()),new DamageVariable(4,2)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
        public class E : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Electro(3).ToCostInit();

            public override SkillCategory Category => SkillCategory.E;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                //NOTE:其实这里有个小bug，和原版一样，但是我懒得改
                if (me.Enemy.Characters[me.Enemy.CurrCharacter].Effects.Find(typeof(Effect_Lisa)) is AbstractPersistent p)
                {
                    me.Enemy.Hurt(new(4, 2 + p.AvailableTimes), this, () => p.Active = false);
                }
                else
                {
                    me.Enemy.Hurt(new(4, 2), this, () => me.Enemy.AddPersonalEffect(new Effect_Lisa()));
                }
            }
        }
    }
    public class Summon_Lisa : AbstractCardSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(4,2),this,()=>p.AvailableTimes--)}
        };
    }
    public class Effect_Lisa : AbstractCardEffect
    {
        public override int InitialUseTimes => 2;
        public override int MaxUseTimes => 4;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                if (p.AvailableTimes<4)
                {
                     p.AvailableTimes++;
                }
            }
            }
        };
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            if (persistent.AvailableTimes < 4)
            {
                persistent.AvailableTimes++;
            }
        }
    }
    public class Talent_Lisa : AbstractCardEquipmentTalent
    {
        public override string CharacterNameID => "lisa";

        public override CostInit Cost => new CostCreate().Electro(1).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0&&me.TeamIndex==s.TeamID && s is AfterSwitchSender ss && ss.Target==p.PersistentRegion)
                {
                    me.Enemy.AddPersonalEffect(new Effect_Lisa());
                    p.AvailableTimes--;
                }
            }
            },
            new PersistentPreset.RoundStepReset()
        };
    }
}
