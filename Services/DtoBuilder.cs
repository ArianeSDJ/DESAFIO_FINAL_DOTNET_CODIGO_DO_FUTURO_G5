
using desafio_dotnet.Models;
using desafio_dotnet.DTOs;

namespace desafio_dotnet.Services;

public class DtoBuilder<T>
{
    public static T Builder(object objetoDto)
    {
        var novoObj = Activator.CreateInstance<T>();
        
        foreach(var propriedadesDTO in objetoDto.GetType().GetProperties())
        {
            var propriedadesObj = novoObj?.GetType().GetProperty(propriedadesDTO.Name);
            if(propriedadesObj is not null)
            {
                propriedadesObj.SetValue(novoObj, propriedadesDTO.GetValue(objetoDto));
            }
        }
        return novoObj;
    }
}