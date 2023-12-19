using ALLinONE.Shared; // Protector
using System.Security; // SecurityException
using System.Security.Principal; // IPrincipal
using System.Security.Claims;
using System.Xml;
using System.Security.Cryptography;
using System.Text;




XmlDocument doc = new();
string xmlPath = "SensitiveData.xml";

doc.Load(xmlPath);

XmlNode nameNode = doc.DocumentElement.SelectSingleNode("/customers/customer/name");
XmlNode creditCardNode = doc.DocumentElement.SelectSingleNode("/customers/customer/creditcard");
XmlNode passwordNode = doc.DocumentElement.SelectSingleNode("/customers/customer/password");
     
WriteLine("Name: " + nameNode.InnerText);
WriteLine("Credit Card: " + creditCardNode.InnerText);
WriteLine("Password: " + passwordNode.InnerText);


string creditCardNumber = creditCardNode.InnerText;
string password = passwordNode.InnerText;
string salt = "7BANANAS";

string saltedPassword = password + salt;

SHA256 sHA256 = SHA256.Create();
byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

StringBuilder builder = new();
for(int i =0; i < bytes.Length; i++)
{
    builder.Append(bytes[i].ToString("x2"));
}

WriteLine("Salted and Hashed password: " + builder.ToString());


if(passwordNode is not null)
{
    passwordNode.InnerText = builder.ToString();
    doc.Save(xmlPath);
    WriteLine("Tekst został zapisany do pliku XML.");
}
else
{
    WriteLine("Nie znaleziono węzła.");
}

/*
string cipherText = Protector.Encrypt(creditCardNumber, password);

WriteLine($"Encrypted text: {cipherText}");

if(creditCardNode is not null)
{
    creditCardNode.InnerText = cipherText;
    doc.Save(xmlPath);
    WriteLine("Tekst został zapisany do pliku XML.");
}
else
{
    WriteLine("Nie znaleziono węzła.");
}
*/

/*
Write("Enter the password: ");
string? password2Decrypt = ReadLine();

if(password2Decrypt is null)
{
    WriteLine("Password to decrypt cannot be null.");
    return;
}

try
{
    string clearText = Protector.Decrypt(cipherText, password2Decrypt);
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
*/
/*
using(SHA256 sha256Hash = SHA256.Create())
{
    // Konwersja na bajty i obliczenie hasha
    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(creditCardNumber));

    // Konwersja bajtów na ciąg znaków
    StringBuilder builder = new();
    for(int i = 0; i < bytes.Length; i++)
    {
        builder.Append(bytes[i].ToString("x2"));
    }

    WriteLine("Hashed Credit Card Number: " + builder.ToString());
}

// Wygeneruj sól
byte[] salt = new byte[16];

using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(salt);
}

// Dodaj sól do hasła
string saltedPassword = password + Convert.ToBase64String(salt);

using (SHA256 sha256Hash = SHA256.Create())
{
    // Konwersja na bajty i obliczenie hasła
    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

    // Konwersja bajtów na ciąg znaków
    StringBuilder builder = new();
    for(int i = 0; i < bytes.Length;  i++) 
    {
        builder.Append(bytes[i].ToString("x2"));
    }

    WriteLine("Hashed and Salted Password: " + builder.ToString());
}
*/

