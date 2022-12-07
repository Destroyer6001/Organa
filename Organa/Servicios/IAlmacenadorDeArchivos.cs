namespace Organa.Servicios
{
    public interface IAlmacenadorDeArchivos
    {
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta,
        string contentType);
        Task BorrarArchivo(string ruta, string contenedor);
        Task<string> GuardarArchivo(string contenedor, IFormFile foto);
    }
}
