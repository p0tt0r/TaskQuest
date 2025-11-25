using System;
using System.Security.Cryptography;

/*
 Пишем код через общение микросервисов причем микросервисы общаются через rest API
 
 */


namespace Sourse
{
    internal class Program
    {
        static void Main(string[] args)
        {

            User vova = new User();

            Console.Write("Id: ");
            int id_1 = Convert.ToInt32(Console.ReadLine());

            vova.chengeId(id_1);

            Console.WriteLine(vova.GetID());



        }
    }
}
