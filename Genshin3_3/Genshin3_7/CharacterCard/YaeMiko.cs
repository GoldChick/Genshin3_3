using TCGBase;

namespace Genshin3_7
{
    public class Yae : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
        new CharacterSimpleA(4,1),
        new CharacterSingleSummonE(new Summon_Yae(),4),
        new Q()
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        private class Q : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Electro(3).MP(2).ToCostInit();
            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new DamageVariable(4, 4, 0), this, () =>
                {
                    var s = me.Summons.Find(typeof(Summon_Yae));
                    if (s != null)
                    {
                        s.Active = false;
                        me.AddPersistent(new Effect_Yae());
                    }
                });
            }
        }
    }
    public class Effect_Yae : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundMeStart,(me,p,s,v)=>me.Enemy.Hurt(new(4,3),this,()=>p.Active=false)}
        };
    }

    public class Summon_Yae : AbstractCardPersistentSummon
    {
        public override int InitialUseTimes => 3;
        public override int MaxUseTimes => 6;
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            if (persistent.AvailableTimes < MaxUseTimes)
            {
                persistent.AvailableTimes = int.Min(persistent.AvailableTimes + 3, MaxUseTimes);
            }
        }

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {SenderTag.AfterPass.ToString(),(me,p,s,v)=>
            {
                if (p.AvailableTimes>3 &&s.TeamID==me.TeamIndex)
                {
                    me.Enemy.Hurt(new(4, 1, 0), this,()=>p.AvailableTimes--);
                }}},
            { SenderTag.RoundOver.ToString(),(me, p, s, v) => me.Enemy.Hurt(new(4, 1, 0), this,()=>p.AvailableTimes --) }
        };
    }

    public class Effect_Yae_T : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.E,
                4,2)
            }
        };
    }

    public class Talent_Yae : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;
        public override string CharacterNameID => "yae";
        public override CostInit Cost => new CostCreate().Electro(3).MP(2).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new DamageVariable(4, 4, 0), c.Card.Skills[2], () =>
            {
                var s = me.Summons.Find(typeof(Summon_Yae));
                if (s != null)
                {
                    s.Active = false;
                    me.AddPersistent(new Effect_Yae());
                    me.AddPersistent(new Effect_Yae(), c.Index);
                }
            });
        }
    }
}
