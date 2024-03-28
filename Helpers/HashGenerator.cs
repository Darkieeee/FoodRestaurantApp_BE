using System.Security.Cryptography;

class HashGenerator {
    public static HMAC CreateHMAC(string algorithm, byte[] key) {
        if(algorithm.Equals("SHA256")) {
            return new HMACSHA256(key);
        } else if(algorithm.Equals("MD5")) {
            return new HMACMD5(key);
        } else {
            throw new ArgumentException("Algorithm is not found or not supported yet");
        }
    }

    public static string GenerateUid() {
        return $"UID{DateTime.Now.Ticks}";
    }

    public static string GenerateGuid() {
        return Guid.NewGuid().ToString();
    }
}