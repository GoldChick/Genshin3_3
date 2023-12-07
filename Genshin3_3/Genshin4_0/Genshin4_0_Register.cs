using TCGBase;

namespace Genshin4_0
{
    public class Genshin4_0_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Talent_Lisa());
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Lisa());
            consumer.Accept(new Qiqi());
        }
    }
}
