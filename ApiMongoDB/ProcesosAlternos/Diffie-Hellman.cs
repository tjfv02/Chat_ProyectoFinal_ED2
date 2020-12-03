using System;
using System.Collections.Generic;
using System.Text;

namespace ProcesosAlternos
{
    public static class Diffie_Hellman
    {

        public static int GenerarLlaves(int key1, int key2)
        {
            int aux = 349;
            for (int i = 1; i < key1; i++)
            {
                aux *= 349;
                aux %= 777;
            }
            int key = aux;
            for (int i = 1; i < key2; i++)
            {
                key *= aux;
                key %= 777;
            }
            return key;
        }
    }
}
