//using TCGBase;

//namespace Genshin3_3
//{
//    public class Nahida : AbstractCardCharacter
//    {
//        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
//        new CharacterSimpleA(6,1),
//        new 所闻编辑真如()
//        };

//        public override ElementCategory CharacterElement => ElementCategory.Dendro;

//        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

//        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;

//        public override string NameID => "nahida";
//        public class 所闻编辑 : AbstractCardSkill
//        {
//            public override int[] Costs => throw new NotImplementedException();

//            public override SkillCategory Category => throw new NotImplementedException();

//            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
//            {
//                throw new NotImplementedException();
//            }
//        }
//        public class 所闻编辑真如 : AbstractCardSkill
//        {
//            public override int[] Costs => new int[] { 1 };

//            public override SkillCategory Category => SkillCategory.E;

//            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
//            {
//                for (int i = 0; i < me.Enemy.Characters.Length; i++)
//                {
//                    me.Enemy.AddPersistent(new 怨种印(), i);
//                }
//                me.Enemy.Hurt(new DamageVariable(6, 3, 0), this);
//            }
//        }
//        public class 怨种印 : AbstractCardPersistent
//        {
//            private readonly static string 怨种印触发 = "genshin3_3:怨种印触发";
//            public override int MaxUseTimes => 2;

//            public override PersistentTriggerDictionary TriggerDic => new()
//            {
//                {
//                    SenderTag.AfterHurt,
//                    (me, p, s, v) => { if(s is HurtSender hs && hs.Reaction!=ReactionTags.None) { if(hs.TeamID == me.TeamIndex && hs.TargetIndex == p.PersistentRegion) { p.AvailableTimes --; p.Data = 1; me.Hurt(new DamageVariable(-1, 1, 0), this); me.EffectTrigger(me.Game, me.TeamIndex, new SimpleSender(怨种印触发)); } } }
//                },
//                {
//                    怨种印触发,
//                    (me, p, s, v) => { if(p.Data != null && p.Data.Equals(1)) { p.Data = null; } else { p.AvailableTimes --; p.Data = 1; me.Hurt(new DamageVariable(-1, 1, p.PersistentRegion - me.CurrCharacter), this); } }
//                }
//            };

//            public override string Namespace => "genshin3_3";
//            public override string NameID => "effect_nahida";
//        }
//    }
//}
