using Application.Services.Cryptography;

namespace Utils.PasswordEncryptor;

public class PasswordEncryptorBuilder
{
    public static Encryptor Instance()
    {
        return new Encryptor("AliceInChains");
    }
}