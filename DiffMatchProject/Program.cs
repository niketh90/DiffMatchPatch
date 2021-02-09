using System;
using System.Collections.Generic;
using System.Text;
using DiffMatchPatch;

namespace DiffMatchProject
{
    class Program
    {
        static void Main(string[] args)
        {
            diff_match_patch diffEngine = new diff_match_patch();
            StringBuilder concat = new StringBuilder();
            var x = "<p>O índice do mercado de ações de Hanói, capital do Vietnã, o HNX-Index, fechou a 99,97 pontos na terça-feira, uma queda de 0,88 pontos, ou 0,87 por cento, em relação ao pregão anterior.</p><p> Cerca de 37,8 milhões de ações no valor de 559,1 bilhões de dong vietnamitas (24,3 milhões de dólares) foram negociadas na Bolsa de Valores de Hanói na terça-feira. Especificamente, estrangeiro</p><p/><p> investidores compraram 1,1 milhão de ações avaliadas em quase 13,1 bilhões de dong vietnamitas (568.696 dólares americanos) e venderam 2,9 milhões de ações avaliadas em mais de 69,5 bilhões</p><p/><p> Dong vietnamita (mais de 3 milhões de dólares americanos).</p><p> Os preços de 52 ações subiram, 82 ações caíram, enquanto 233 ações permaneceram inalteradas. Enditem</p>";
            var y = "<p>O índice do mercado de ações de Hanói, capital do Vietnã, o HNX-Index, fechou a 99,97 pontos na terça-feira, uma queda de 0,88 pontos, ou 0,87 por cento, em relação ao pregão anterior.</p><p> Cerca de 37,8 milhões de ações no valor de 559,1 bilhões de dong vietnamitas (24,3 milhões de dólares) foram negociadas na Bolsa de Valores de Hanói na terça-feira. Especificamente, estrangeiro</p><p/><p> investidores compraram 1,1 milhão de ações avaliadas em quase 13,1 bilhões de dong vietnamitas (568.696 dólares americanos) e notrem 2,9 milhões de ações avaliadas em mais de 69,5 bilhões</p><p/><p> Dong vietnamita (mais de 3 milhões de dólares americanos).</p><p> Os lasco de 52 ações subiram, 82 ações caíram, enquanto 233 ações permaneceram inalteradas. Enditem</p>";

            var diffWord = diffEngine.diff_wordMode(x,y, DateTime.MaxValue);

            foreach (var d in diffWord)
            {  switch (d.operation)
                {
                    case Operation.DELETE:
                        concat.Append(d.text+"\t");
                        break;
                    case Operation.EQUAL:
                        //concat.Append(d.text);
                        break;
                    case Operation.INSERT:
                        concat.Append(d.text +"\n");
                        break;
                    default:
                        break;
                }
            }

            System.IO.File.WriteAllText(@"/tmp/WordList.xls", concat.ToString());
            concat.Clear();

            var a = diffEngine.diff_linesToChars(x.Replace("<p>"," ").Replace("<p/>","\n").Replace("</p>","\n"), y.Replace("<p>", " ").Replace("<p/>", "\n").Replace("</p>", "\n"));
            var text1 = a[0];
            var text2 = a[1];
            List<string> linearray = (List<string>)a[2];
            var diffs = diffEngine.diff_main((string)text1, (string)text2, true,DateTime.MaxValue);
            diffEngine.diff_charsToLines(diffs, linearray);
            foreach (var d in diffs)
            {
                switch (d.operation)
                {
                    case Operation.DELETE:
                        concat.Append(d.text.Replace("\n","").Trim() + "\t");
                        break;
                    case Operation.EQUAL:
                        //concat.Append(d.text);
                        break;
                    case Operation.INSERT:
                        concat.Append(d.text.Replace("\n", "").Trim() + "\n");
                        break;
                    default:
                        break;
                }
            }
            System.IO.File.WriteAllText(@"/tmp/PhraseList.xls", concat.ToString());
        }
    }
}
