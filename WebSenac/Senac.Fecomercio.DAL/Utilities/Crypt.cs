using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace CieloGtecDAL.Crypt
{
    /// <summary>
    /// Enumerator com os tipos de classes para criptografia.
    /// </summary>
    public enum CryptProvider
    {
        /// <summary>
        /// Representa a classe base para implementações criptografia dos algoritmos simétricos Rijndael.
        /// </summary>
        Rijndael,
        /// <summary>
        /// Representa a classe base para implementações do algoritmo RC2.
        /// </summary>
        RC2,
        /// <summary>
        /// Representa a classe base para criptografia de dados padrões (DES - Data Encryption Standard).
        /// </summary>
        DES,
        /// <summary>
        /// Representa a classe base (TripleDES - Triple Data Encryption Standard).
        /// </summary>
        TripleDES
    }

    /// <summary>
    /// Classe auxiliar com métodos para criptografia de dados.
    /// </summary>
    public class Crypt
    {
        #region Private members
		private string _key = string.Empty;
        PasswordDeriveBytes pdb = null;
        private CryptProvider _cryptProvider;
        private SymmetricAlgorithm _algorithm;
        private void SetIV()
        {
            //_algorithm.BlockSize = 128; //128 bit BlockSize
            //_algorithm.Padding = PaddingMode.PKCS7;

            switch (_cryptProvider)
            {
                case CryptProvider.Rijndael:
                    //_algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    _algorithm.IV = pdb.GetBytes(16);
                    break;
                default:
                    //_algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    _algorithm.IV = pdb.GetBytes(8);
                    break;
            }
        }
	    #endregion

        #region Properties
        /// <summary>
        /// Chave secreta para o algoritmo simétrico de criptografia.
        /// </summary>
		public string Key
		{
			get { return _key; }
			set 
            { 
                _key = value;
                //pdb = new PasswordDeriveBytes(_key, new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(_key)));
                //byte[] key = pdb.GetBytes(32);
                //byte[] IV = pdb.GetBytes(16);
            }
		}
        #endregion

        #region Constructors
        /// <summary>
        /// Contrutor padrão da classe, é setado um tipo de criptografia padrão.
        /// </summary>
        public Crypt()
        {
            _algorithm = new RijndaelManaged();
            _algorithm.Mode = CipherMode.CBC;
            _cryptProvider = CryptProvider.Rijndael;
            //Key = "Qp1DJDZNgvJrFXv5mYPDttgkBVNPaa";
            Key = "_EAU)¨z&May¨rX3[tk%CMXuO1T6{&G";
        }
        /// <summary>
        /// Construtor com o tipo de criptografia a ser usada.
        /// </summary>
        /// <param name="cryptProvider">Tipo de criptografia.</param>
        public Crypt(CryptProvider cryptProvider)
		{	
			// Seleciona algoritmo simétrico
			switch(cryptProvider)
			{
				case CryptProvider.Rijndael:
					_algorithm = new RijndaelManaged();
					_cryptProvider = CryptProvider.Rijndael;
					break;
				case CryptProvider.RC2:
					_algorithm = new RC2CryptoServiceProvider();
					_cryptProvider = CryptProvider.RC2;
					break;
				case CryptProvider.DES:
					_algorithm = new DESCryptoServiceProvider();
					_cryptProvider = CryptProvider.DES;
					break;
				case CryptProvider.TripleDES:
					_algorithm = new TripleDESCryptoServiceProvider();
					_cryptProvider = CryptProvider.TripleDES;
					break;
			}
			_algorithm.Mode = CipherMode.CBC;
		}
        #endregion

        #region Public methods
        /// <summary>
        /// Gera a chave de criptografia válida dentro do array.
        /// </summary>
        /// <returns>Chave com array de bytes.</returns>
        public virtual void SetKey()
        {
            string salt = string.Empty;

            // Ajuta o tamanho da chave se necessário e retorna uma chave válida
            if (_algorithm.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits
                int keySize = _key.Length * 8;
                int minSize = _algorithm.LegalKeySizes[0].MinSize;
                int maxSize = _algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = _algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    _key = _key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        _key = _key.PadRight(validSize / 8, '*');
                    }
                }
            }
            //pdb = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(_key));
            pdb = new PasswordDeriveBytes(_key, new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(_key)));

            // Seta a chave privada
            _algorithm.Key = pdb.GetBytes(_key.Length);
            SetIV();
        }

        public virtual ICryptoTransform GetEncryptor()
        {
            SetKey();

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();

            return cryptoTransform;
        }

        public virtual ICryptoTransform GetDecryptor()
        {
            SetKey();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

            return cryptoTransform;
        }

        /// <summary>
        /// Encripta o dado solicitado.
        /// </summary>
        /// <param name="plainText">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado.</returns>
        public virtual string Encrypt(string plainText)
        {
            //byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(plainText);
            byte[] plainByte = UnicodeEncoding.Unicode.GetBytes(plainText);

            SetKey();

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();

            MemoryStream _memoryStream = new MemoryStream();

            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }
        /// <summary>
        /// Desencripta o dado solicitado.
        /// </summary>
        /// <param name="cryptoText">Texto a ser descriptografado.</param>
        /// <returns>Texto descriptografado.</returns>
        public virtual string Decrypt(string cryptoText)
        {
            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(cryptoText);

            SetKey();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();
            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);

                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                //// Busca resultado do CryptoStream
                //StreamReader _streamReader = new StreamReader(_cryptoStream);
                //return _streamReader.ReadToEnd();

                //byte[] buffer = new byte[_cryptoStream.Length];
                //_cryptoStream.Read(buffer, 0, (int)_cryptoStream.Length);
                //return UnicodeEncoding.Unicode.GetString(buffer);

                //MemoryStream _mem = new MemoryStream();
                //// Este método só existe a partir do framework 4
                //_cryptoStream.CopyTo(_mem);
                //return UnicodeEncoding.Unicode.GetString(_mem.ToArray());

                StringBuilder ret = new StringBuilder();
                byte[] buffer = new byte[512];
                while (true)
                {
                    int n = _cryptoStream.Read(buffer, 0, buffer.Length);
                    if (n <= 0)
                        break;

                    ret.Append(UnicodeEncoding.Unicode.GetString(buffer, 0, n));
                }
                return ret.ToString();
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
