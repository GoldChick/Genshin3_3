using TCGBase;

namespace Genshin3_4
{
    public class Genshin3_4_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Talent_Klee());
            consumer.Accept(new Talent_Beidou());
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Klee());
            consumer.Accept(new Beidou());
        }
    }
}
