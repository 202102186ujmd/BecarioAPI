using BecarioAPI.Models.Solicitantes;
using System.ComponentModel.DataAnnotations;

namespace BecarioAPI.Models.Solicitudes
{
    //validacion para que no se repitan las notas
    #region Validacion para que no se repitan las notas
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoDuplicadosSolicitanteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (BecarioDBContext)validationContext.GetService(typeof(BecarioDBContext));
            var solicitud = (Solicitud)validationContext.ObjectInstance;

            // Validamos que no exista un solicitante con los mismos valores
            if (dbContext.Solicitudes.Any(s =>
                s.IdSolicitud == solicitud.IdSolicitud))
      
            {
                return new ValidationResult("Ya existe una solicitud con los mismos valores.");
            }

            return ValidationResult.Success;
        }
    }
    #endregion
    #region Fecha futura
    public class FechaFuturaValidaAttribute : ValidationAttribute
    {
        //Validacion de la fecha de nacimiento no sea mayor a la fecha actual
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? fechaNacimiento = (DateTime?)value;

            if (fechaNacimiento.HasValue && fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult("La fecha no puede estar en el futuro.");
            }

            return ValidationResult.Success;
        }
    }
    #endregion
    public class Solicitud
    {
        [Key]
        public int IdSolicitud { get; set; }
        [Required]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe una solicitud con los mismos valores.")]
        public int IdSolicitante { get; set; }
        [FechaFuturaValida(ErrorMessage = "La fecha no puede estar en el futuro.")]
        public DateTime FechadeCreacion { get; set; }
        public int IdEstado { get; set; } = 1;

        public Solicitud()
        {
            // Establecer la fecha de creación al momento de la instancia
            FechadeCreacion = DateTime.Now;
        }
    }
}
