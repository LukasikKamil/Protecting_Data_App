﻿using System.Security.Cryptography; // CryptographicException
using ALLinONE.Shared; // Protector

Write("Enter a message that you want to encrypt: ");
string? message = ReadLine();

Write("Enter a password: ");
string? password = ReadLine();

if ((password is null) || (message is null))
{
    WriteLine("Message or password cannot be null.");
    return;
}

string cipherText = Protector.Encrypt(message, password);

WriteLine($"Encrypted text: {cipherText}");

Write("Enter the password: ");
string? password2Decrypt = ReadLine();

if (password2Decrypt is null)
{
    WriteLine("Password to decrypt cannot be null.");
    return;
}

try
{
    string clearText = Protector.Decrypt(cipherText, password2Decrypt);
    WriteLine($"Decrypted text: {clearText}");
}
catch (CryptographicException)
{
    WriteLine("You entered the wrong password!");
}
catch (Exception ex)
{
    WriteLine("Non-cryptographic exception: {0}, {1}",
        ex.GetType().Name, ex.Message);
}
