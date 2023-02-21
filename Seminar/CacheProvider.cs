using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace Seminar
{
    public class BaseCacheProviderExeption : Exception
    {

    }

    public class SerializeDataCacheProviderExeption : BaseCacheProviderExeption { }
    public class DeserializeDataCacheProviderExeption : BaseCacheProviderExeption { }

    public class ProtectCacheProviderExeption : BaseCacheProviderExeption { }

    public class UnprotectCacheProviderExeption : BaseCacheProviderExeption { }
    public class CacheProvider
    {
        static byte[] additionalEntropy = {5, 3, 7, 1, 5 };
        public void CacheConnection(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                using MemoryStream memoryStream = new MemoryStream();
                using XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlTextWriter, connections);
                byte[] prodectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes("data.protected", prodectedData);
            }  
            catch(Exception e) 
            { 
                Console.WriteLine("Serialize data error."); 
                throw new SerializeDataCacheProviderExeption(); 
            }


        }

        private byte[] Protect(byte[] data)
        {

            try 
            { 
            return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Protected errot.");
                throw new ProtectCacheProviderExeption();
            }
        }

        public List<ConnectionString> GetConnectionsFromCache()
        {
            try
            {

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));

            byte[] protectedData  = File.ReadAllBytes("data.protected");
            byte[] data = Unprotect(protectedData);
            return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Deserialize data errot.");
                throw new DeserializeDataCacheProviderExeption();
            }
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unprotected errot.");
                throw new UnprotectCacheProviderExeption();
            }

        }
    }
}
