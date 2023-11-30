using TCGBase;

namespace Genshin3_7
{
    public class Genshin_3_7_Util : AbstractModUtil
    {
        public override string NameSpace => "genshin3_7";

        public override string Description => "genshin tcg 2.0";

        public override string Author => "Gold_Chick";

        public override string[] GetDependencies()=>Array.Empty<string>();

        public override AbstractRegister GetRegister() => new Genshin3_7_Register();
    }
}
