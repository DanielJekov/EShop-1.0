namespace SandBox
{
    using System;
    using System.IO;
    using System.Text;

    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().Location);// Filename
        }

        private static void NewMethod()
        {
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            string text = System.IO.File.ReadAllText(@"../../../LoremIpsum.txt");
            tw.Write(text, 0, text.Length);
            tw.Close();

            byte[] temp = GetBufferCustom(text);

            Console.WriteLine(string.Join(string.Empty, temp));

            Console.WriteLine();
        }

        /// <summary>
        /// Returns the array of unsigned bytes from which this stream was created.
        /// </summary>
        /// <returns>The byte array from which this stream was created.
        /// </returns>
        public static byte[] GetBufferCustom(string input)
        {
            int buffer = input.Length - 1;

            byte[] bytes = new byte[buffer];
            for (int i = 0; i < buffer; i++)
            {
                bytes[i] = (byte)input[i];
            }

            return bytes;
        }
    }

}
