using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectConverter
{
    public class AutoConverter
    {
        /// <summary>
        /// Método faz a conversão do objeto passado por parâmetro de origem para o objeto de destino.
        /// </summary>
        /// <typeparam name="TOrigem"></typeparam>
        /// <typeparam name="TDestino"></typeparam>
        /// <param name="objetoOrigem"></param>
        /// <returns>retorna objeto de destino</returns>
        public static TDestino ToConvert<TOrigem, TDestino>(TOrigem objetoOrigem)
        {
            var attributesObjectOrigem = typeof(TOrigem).GetProperties();
            var attributesObjectDestino = typeof(TDestino).GetProperties();
            var objetoDestino = Activator.CreateInstance(typeof(TDestino));

            foreach (var attributeOrigem in attributesObjectOrigem)
            {
                foreach (var attributeDestino in attributesObjectDestino)
                {
                    if (attributeOrigem.Name == attributeDestino.Name && attributeOrigem.GetType() == attributeDestino.GetType())
                    {
                        var propertyInfo = objetoDestino.GetType().GetProperty(attributeDestino.Name);
                        if (propertyInfo != null)
                        {
                            if (attributeOrigem.PropertyType.IsAssignableFrom(attributeDestino.PropertyType))
                            {
                                propertyInfo.SetValue(objetoDestino, attributeOrigem.GetValue(objetoOrigem, null), null);
                                break;
                            }
                            Type tipoOrigem = attributeOrigem.PropertyType.GetGenericArguments()[0];
                            Type tipoDestino = attributeDestino.PropertyType.GetGenericArguments()[0];

                            if (tipoOrigem.IsClass)
                            {
                                var lista = (ICollection)attributeOrigem.GetValue(objetoOrigem, null);

                                var tipoEspecifico = typeof(List<>).MakeGenericType(new Type[] { tipoDestino });
                                var novaLista = (IList)Activator.CreateInstance(tipoEspecifico);

                                foreach (var objeto in lista)
                                {
                                    var methodInfo = typeof(AutoConverter).GetMethod("ToConvert");
                                    if (methodInfo != null)
                                    {
                                        var metodo = methodInfo.MakeGenericMethod(tipoOrigem, tipoDestino);
                                        var objetoConvertido = metodo.Invoke(null, new object[] { objeto });
                                        novaLista.Add(objetoConvertido);
                                    }
                                }
                                propertyInfo.SetValue(objetoDestino, novaLista, null);
                            }
                        }
                    }
                }
            }
            return (TDestino)objetoDestino;
        }
    }
}
