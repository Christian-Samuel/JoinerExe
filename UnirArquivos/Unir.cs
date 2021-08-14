using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnirArquivos
{
    static class Unir
    {
        public static void unir(string arq1, string arq2,string nome)
        {
            var arquivoBase = File.ReadAllBytes("arqBase.arq");

            var arquivo1 = File.ReadAllBytes(arq1);
            var arquivo2 = File.ReadAllBytes(arq2);
            var arquivo3 = new byte[0];

            byte[] limites = { 170, 187, 204, 221, 238, 255 };

            arquivo3 = arquivo3.Concat(arquivoBase).ToArray();
            arquivo3 = arquivo3.Concat(limites).ToArray();
            arquivo3 = arquivo3.Concat(arquivo1).ToArray();
            arquivo3 = arquivo3.Concat(limites).ToArray();
            arquivo3 = arquivo3.Concat(arquivo2).ToArray();

            File.WriteAllBytes(nome == "" ? "File":nome+ ".exe", arquivo3);
        }

    }
}
