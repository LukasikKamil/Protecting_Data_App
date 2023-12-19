using ALLinONE.Shared; // Protector
using System.Xml;
using System.Security.Cryptography;
using System.Text;

XmlDocument doc = new();
string xmlPath = "SensitiveData.xml";

doc.Load(xmlPath);

XmlNode? nameNode = doc.DocumentElement?.SelectSingleNode("/customers/customer/name");
XmlNode? creditCardNode = doc.DocumentElement?.SelectSingleNode("/customers/customer/creditcard");
XmlNode? passwordNode = doc.DocumentElement?.SelectSingleNode("/customers/customer/password");

WriteLine("Name: " + nameNode?.InnerText);
WriteLine("Credit Card: " + creditCardNode?.InnerText);

string? creditCardEncrypt = creditCardNode?.InnerText;
string salt = "7BANANAS";
string? passwordSaltAndHashed = passwordNode?.InnerText;



Write("Enter the password: ");
string? password2Decrypt = ReadLine();
string password2 = HashPassword(password2Decrypt, salt);


if(password2Decrypt is null)
{
    WriteLine("Password to decrypt cannot be null.");
    return;
}

if(passwordSaltAndHashed == password2)
{
    try
    {
        string? clearText = Protector.Decrypt(creditCardEncrypt, password2Decrypt);
        WriteLine($"Decrypted text: {clearText}");
    }
    catch(CryptographicException)
    {
        WriteLine("You entered the wrong password!");
    }
    catch(Exception ex)
    {
        WriteLine("Non-cryptographic exception: {0}, {1}",
            ex.GetType().Name, ex.Message);
    }
}
else
{
    WriteLine("XXXX-XXXX-XXXX-XXXX");
}

static string HashPassword(string password, string salt)
{
    string saltedPassword = password + salt;

    SHA256 sha256 = SHA256.Create();
    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

    StringBuilder builder = new StringBuilder();
    for(int i = 0; i < bytes.Length; i++)
    {
        builder.Append(bytes[i].ToString("x2"));
    }

    return builder.ToString();
}