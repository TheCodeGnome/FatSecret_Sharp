using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Service4u2.Json
{
    public class MySurrogate<TInner> : IDataContractSurrogate
    {
        #region IDataContractSurrogate Members

        public System.Type GetDataContractType(System.Type type)
        {
            if (typeof(TInner).IsAssignableFrom(type))
            {
                return typeof(List<TInner>);
            }

            return type;
        }

        public object GetObjectToSerialize(object obj, System.Type targetType)
        {
            // This method is called on serialization.
            

            return obj;
        }

        public object GetDeserializedObject(object obj, System.Type targetType)
        {
            // This method is called on deserialization.
            if (obj is List<TInner>)
            {
                // This is going to fail...
                List<TInner> list = new List<TInner>()
                {
                    (TInner)obj
                };

                return list;
            }
            return obj;
        }

        public void GetKnownCustomDataTypes(System.Collections.ObjectModel.Collection<System.Type> customDataTypes)
        {
            return;
        }        

        public System.Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            return null;
        }

        public System.CodeDom.CodeTypeDeclaration ProcessImportedType(System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit)
        {
            return null;
        }

        public object GetCustomDataToExport(System.Type clrType, System.Type dataContractType)
        {
            return null;
        }

        public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, System.Type dataContractType)
        {
            return null;
        }

        #endregion
    }


    public static class JsonHelper
    {
        public static T Deserialize<T>(string jsonString)
        {
            using(MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                
                T val;
                try
                {
                    val = (T)serializer.ReadObject(ms);
                }
                catch
                {
                    val = default(T);                    
                }

                return val;
            }
        }
    }
}
