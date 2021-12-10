using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Sharprompt;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace PassManager
{
    public partial class Program {
        //tiny.cc/hectorsolar99
        //bit.ly/hectorsolar99

        #region variables
        public static Regex yesPattern = new(@"[ok]||[accept]||[true]||[y*]||[s*]");
        #endregion

        static void Main(string[] args) {

            try
            {

                Console.WriteLine("Welcome user thanks for using password manager");

                String PrivateKey = GetPrivateKey();
                Console.WriteLine("Private Key Entered");

                List<string> PublicKeys = new();


                String option = Prompt.Input<String>("Load public keys?");

                //loaded from enumeration 
                List<string> currentValuesAdded = Enum.GetValues(typeof(PublicKeys)).Cast<PublicKeys>().ToList().Select(x => x.ToString()).ToList();

                

                PublicKeys = yesPattern.IsMatch(option)|| string.IsNullOrEmpty(option) ?
                    //load saved values
                    Prompt.MultiSelect("Passwords to encrypt at a time?", currentValuesAdded.ToArray(), pageSize: 10).ToList()
                    :
                    //enter others
                    Prompt.List<string>("Please add passwords(Enter to finish)").ToList();


                List<Func<String, String, String>> funciones = new() { Encryptor.EncryptNumberPassword, Encryptor.EncryptStringPassword };
                MostrarResultados(PublicKeys, PrivateKey, funciones);










            }
            catch (Exception e) { Console.WriteLine($"Error! {e.Message}"); }
            finally
            {
                Console.WriteLine("Done! ,Press any key to close...");
                _ = Console.ReadLine();
            }

        }

        public static string GetPrivateKey()
        {
            string PrivateKey = Prompt.Password("Enter private key", '*', new[] { Validators.Required() });
            String CheckPrivateKey = Prompt.Input<String>("Do you to check your pk? (highly recommended if you are cyphering a new password) y-n ");

            bool confirmar = true;
            if (yesPattern.IsMatch(PrivateKey))
            {
                switch (  Prompt.Select("Options to check:", new List<String> { "show it", "enter again" , "cypher with other PublicKey" }))
                {
                    case "show it":
                        confirmar= Prompt.Confirm($"Ok, then is it right???? {PrivateKey}");
                        
                        break;
                    case "enter again":
                        string PrivateKey2 = Prompt.Password("Enter private key", '*', new[] { Validators.Required() });
                        confirmar= PrivateKey.Equals(PrivateKey2);
                        break;

                    case "cypher with other PublicKey":
                        String publicKey = Prompt.Input<String>("Enter the public key to cypher");
                        confirmar = Prompt.Confirm($"Ok, then is it right the cyphered???? {Encryptor.EncryptStringPassword(publicKey, PrivateKey)}");
                        break;

                }
            }
            Console.Clear();
            
            return confirmar ? PrivateKey : GetPrivateKey();
        }

        public static void MostrarResultados(List<String> PublicKeys, String PrivateKey,List<Func<String,String,String>> funcionesCifrado)
        {
            Console.WriteLine($"\nYou picked this public Keys: {string.Join(", ", PublicKeys)}");

            PublicKeys.ToList().ForEach(publicKey => {

                Console.WriteLine($"\nKey: {publicKey}");
                foreach (var funcion in funcionesCifrado){
                Console.WriteLine($"Cyphered:{funcion.Method.Name[7..]} {funcion(publicKey, PrivateKey)}");
                }
            });
        }



        //public static void OlderEasyWay() {
        //    Console.Write("ENTER PASSWORD KEY:");
        //    string passk = Console.ReadLine();
        //    Console.Write("ENTER MASTER KEY:");
        //    string mastk = Console.ReadLine();
        //    Console.WriteLine("Results Encryption:\n");
        //    var res = Encryptor.EncryptStringPassword(mastk ,passk);
        //    Console.WriteLine(res);
        //}
       
        enum PublicKeys {
            hectorsolar99,
            github,
            gmail99,
            gmail367,
            linkedin,
            stackoverflow,
            ibercaja

        }

    }
}