using TCGBase;

namespace Genshin3_3
{
    public class Genshin3_3_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new SacrificialSword());

            consumer.Accept(new LeaveItToMe());
            consumer.Accept(new XingTianZhiZhao());

            #region partner
            consumer.Accept(new Paimon());
            consumer.Accept(new Timmie());

            consumer.Accept(new LiuSu());
            consumer.Accept(new Liben());
            consumer.Accept(new ChangTheNinth());
            
            consumer.Accept(new ParametricTransfomer());
            consumer.Accept(new 赌徒());
            #endregion
            #region location
            consumer.Accept(new DawnWinery());
            consumer.Accept(new FavoniusCathedral());
            consumer.Accept(new KnightsOfFavoniusLibrary());

            consumer.Accept(new LiyueHarbor());
            consumer.Accept(new JadeChamber());
            consumer.Accept(new WangshuInn());
            #endregion
            #region food
            consumer.Accept(new SweetChicken());
            consumer.Accept(new MondstadtHashBrown());
            #endregion
            RegisterTalentCard(consumer);
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Ayaka());
            consumer.Accept(new Chongyun());
            consumer.Accept(new Ganyu());
            consumer.Accept(new Kaeya());
            consumer.Accept(new Qiqi());

            consumer.Accept(new Mona());

            consumer.Accept(new Bennett());
            consumer.Accept(new XiangLing());
            consumer.Accept(new Yoimiya());

            consumer.Accept(new Keqing());
            consumer.Accept(new YaeMiko());

            consumer.Accept(new NingGuang());
            consumer.Accept(new 丘丘岩盔王());
            consumer.Accept(new Noel());

            consumer.Accept(new Collei());
            consumer.Accept(new Nahida());

            consumer.Accept(new Sucrose());
        }
        private static void RegisterTalentCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Talent_Ayaka());
            consumer.Accept(new Talent_Kaeya());

            consumer.Accept(new Talent_Ningguang());
        }
    }
}
