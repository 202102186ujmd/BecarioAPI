namespace BecarioAPI.Models.Solicitudes
{
    public interface ISolicitudesRepository
    {
        void AgregarSolicitud(Solicitud solicitud);
        void ActualizarSolicitud(int solicitudid,Solicitud solicitud);
        void EliminarSolicitud(int IdSolicitud);
        Solicitud ObtenerSolicitud(int IdSolicitud);
        List<Solicitud> ObtenerSolicitudes();
    }
}
