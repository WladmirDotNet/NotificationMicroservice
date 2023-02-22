namespace BLLCore.Interfaces
{
    /// <summary>
    /// Núcleo de criptografia
    /// </summary>
    public interface ICritografiaCore
    {
        /// <summary>
        /// Criptografa strings com base na chave configurada no appsettings.json
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        string EncryptString(string clearText);

        /// <summary>
        /// Decriptografa strings com base na chave configurada no appsettings.json
        /// </summary>
        /// <returns></returns>
        string DecryptString(string cipherText);
    }
}
