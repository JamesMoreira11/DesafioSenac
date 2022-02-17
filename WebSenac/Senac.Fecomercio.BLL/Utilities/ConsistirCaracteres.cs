using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Senac.Fecomercio.BLL.Utilities
{
    public class ConsistirCaracteres
    {
        //char[] invalidos = { '#', '@', '%', '¨', '&', '*', ';', '~', '"', '£', '¢', '¬', '§', '+', '=', '°', '>', '<' };
        char[] permitidos = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '-', '/', ':', '\'', ' ', '>', '=' };
        char[] invalidos;
        public ConsistirCaracteres()
        {
            //invalidos = { '#', '@', '%', '¨', '&', '*', ';', '~', '"', '£', '¢', '¬', '§', '+', '=', '°', '>', '<' };
        }

        public bool TemCaracterInvalido(string texto)
        {
            char[] text = texto.ToCharArray();
            if (text.Length > 0)
            {
                invalidos = text.Except(permitidos).ToArray();

                if (invalidos != null && invalidos.Length > 0)
                {
                    ////tratamento especial para sinal de +
                    //if (invalidos.Length == 1 && invalidos[0] == '+')
                    //{
                    //    for (int i = 0, tam = text.Length; i < tam; i++)
                    //    {
                    //        if (text[i] == '+' && i < tam && Regex.Match(text[i + 1].ToString(), @"^[0-9]+$").Success)
                    //        {
                    //            invalidos = null;
                    //            break;
                    //        }
                    //    }
                    //}
                    //else
                        return true;
                }
            }
            return false;
        }

    }
}
