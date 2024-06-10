namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    /// <summary>
    /// Ingenieros a asignar
    /// </summary>
    public class EngineerToAssign
    {
        /// <summary>
        /// Id del ingeniero
        /// </summary>
        public required int EngineerId { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public required string Name {  get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Activo?
        /// </summary>
        public required bool IsActive { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        public required string Region { get; set; }

        /// <summary>
        /// Tipo de ingeniero
        /// </summary>
        public required string EngineerType {  get; set; }
    }
}