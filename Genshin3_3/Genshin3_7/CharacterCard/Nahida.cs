using TCGBase;

namespace Genshin3_7
{
    public class Nahida : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Dendro(1).ToCostInit(),new DamageVariable(6,1)),
            new CharacterEffectE(6,2,new Effect_Nahida_E()),
            new BigE(),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Dendro(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Nahida_Q()),new DamageVariable(6,4)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Dendro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;

        public class BigE : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Dendro(5).ToCostInit();

            public override SkillCategory Category => SkillCategory.E;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new DamageVariable(6, 3, 0), this, () =>
                {
                    me.Enemy.AddPersistent(new Effect_Nahida_E(), me.Enemy.CurrCharacter);
                    me.Enemy.AddPersistent(new Effect_Nahida_E(), me.Enemy.CurrCharacter);
                });
            }
        }
    }
    public class Effect_Nahida_E : AbstractCardPersistent
    {
        private readonly static string Effect_Nahida_E_Trigger = "genshin3_7:effect_nahida_e_trigger";
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {SenderTag.AfterHurt,(me, p, s, v) =>
            {
                if(s is HurtSender hs && hs.Reaction!=ReactionTags.None)
                {
                    if(hs.TeamID == me.TeamIndex && hs.TargetIndex == p.PersistentRegion)
                    {
                        me.Hurt(new DamageVariable(me.Enemy.Effects.Contains(typeof(Effect_Nahida_Q)) && me.Enemy.Characters.Any(c=>c.Effects.Contains(typeof(Talent_Nahida))) && me.Enemy.Characters.Any(c=>c.Card.CharacterElement==ElementCategory.Pyro)?6:-1
                            , 1, p.PersistentRegion - me.CurrCharacter), this,()=>
                        {
                            p.AvailableTimes --;
                            p.Data = 1;
                            me.EffectTrigger(new SimpleSender(Effect_Nahida_E_Trigger));
                        });
                    }
                }
            }
            },
            {Effect_Nahida_E_Trigger, (me, p, s, v) =>
            {
                if(p.Data != null)
                {
                    p.Data = null;
                }
                else
                {
                    me.Hurt(new DamageVariable(-1, 1, p.PersistentRegion - me.CurrCharacter), this,()=>p.AvailableTimes -- );
                }
            }
            }
        };
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            persistent.AvailableTimes = int.Max(persistent.AvailableTimes, MaxUseTimes);
            for (int i = 0; i < me.Characters.Length; i++)
            {
                int index = (i + persistent.PersistentRegion) % me.Characters.Length;
                var e = me.Characters[index].Effects.Find(typeof(Effect_Nahida_E));
                if (e != null)
                {
                    e.AvailableTimes = int.Max(e.AvailableTimes, MaxUseTimes);
                }
                else
                {
                    me.Enemy.AddPersistent(new Effect_Nahida_E(), index);
                }
            }
        }
    }

    public class Effect_Nahida_Q : AbstractCardPersistent
    {
        public Effect_Nahida_Q(bool hydro = false)
        {
            MaxUseTimes = hydro ? 3 : 2;
        }
        public override int MaxUseTimes { get; }

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if(me.TeamIndex==s.TeamID && v is DamageVariable dv && dv.Reaction!=ReactionTags.None)
                {
                    dv.Damage++;
                }
            }
            }
        };
    }
    public class Talent_Nahida : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;

        public override string CharacterNameID => "nahida";

        public override CostInit Cost => new CostCreate().Dendro(3).MP(2).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(6, 4), c.Card.Skills[3], () =>
            {
                bool electro = false;
                bool hydro = false;
                Array.ForEach(me.Characters, c =>
                {
                    switch (c.Card.CharacterElement)
                    {
                        case ElementCategory.Hydro:
                            hydro = true;
                            break;
                        case ElementCategory.Electro:
                            electro = true;
                            break;
                    }
                });
                if (electro)
                {
                    Array.ForEach(me.Enemy.Characters, c =>
                    {
                        var e = c.Effects.Find(typeof(Effect_Nahida_E));
                        if (e != null)
                        {
                            e.AvailableTimes++;
                        }
                    });
                }
                me.AddPersistent(new Effect_Nahida_Q(hydro));
            });
        }
    }
}
