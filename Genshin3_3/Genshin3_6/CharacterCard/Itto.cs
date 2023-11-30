using Genshin3_5;
using TCGBase;

namespace Genshin3_6
{
    public class Itto : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Geo(1).ToCostInit(),new DamageVariable(0,2)),
            new E(),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Geo(3).MP(3).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Itto_Q(),c.Index),new DamageVariable(5,4)),
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Geo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;
        private class E : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Geo(3).ToCostInit();
            public override SkillCategory Category => SkillCategory.E;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(5, 1), this, () =>
                {
                    me.AddPersistent(new Effect_Itto_Passive(), c.Index);
                    me.AddSummon(new Summon_Itto());
                    var s = me.Summons.Find(typeof(Summon_Itto));
                    if (s != null)
                    {
                        me.AddPersistent(new Effect_Itto_E(), c.Index, s);
                    }
                });
            }
        }
    }
    public class Effect_Itto_Passive : AbstractCardPersistent
    {
        public override int InitialUseTimes => 1;
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A && me.GetDices().Sum()%2==0,
                -1,(me,p,s)=>p.AvailableTimes>=2?1:0,false,
                (me,p,s)=>p.Data=114)
            },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (p.Data!=null && v is DamageVariable dv)
                {
                    if (me.Characters[p.PersistentRegion].Effects.Find(-4)?.Data is Dictionary<int,int> dic && dic.TryGetValue(0,out var times) && times>=1)
                    {
                        dv.Damage++;
                    }
                    dv.Damage++;
                    p.Data=null;
                    p.AvailableTimes--;
                }
            }
            }
        };
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            persistent.AvailableTimes = int.Min(MaxUseTimes, persistent.AvailableTimes + 1);
        }
    }
    public class Effect_Itto_E : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is HurtSender hs)
                {
                    me.AddPersistent(new Effect_Itto_Passive(),p.PersistentRegion);
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
    public class Summon_Itto : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 1;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.HurtDecrease,new PersistentPurpleShield(1)},
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(5,1),this,()=>p.Active=false)}
        };
    }
    public class Effect_Itto_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.ElementEnchant,new PersistentElementEnchant(5,false,1)},
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--}
        };
    }
    public class Talent_Itto : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 0;
        public override string CharacterNameID => "itto";
        public override CostInit Cost => new CostCreate().Void(2).Geo(1).ToCostInit();
        //加伤写在effect里面了
    }
}
